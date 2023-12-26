using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Service.Contracts;

namespace sdlt.Service;
internal sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    public readonly IMapper _mapper;
    public ProductService(IRepositoryManager repository, ILoggerManager
    logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<(IEnumerable<ProductDto>? products, MetaData metaData)> GetAllProductsAsync(ProductParameters parameters, bool trackChanges)
    {
        if(!parameters.ValidPriceRange)
            throw new MaxPriceRangeBadRequestException();
            
        var productsWithMetaData = await _repository.Product.GetAllProducts(parameters, trackChanges);
        IEnumerable<ProductDto> productsDto = productsWithMetaData.Select(
            p => new ProductDto(p.Id, p.Name, p.Description, p.Category.Name, p.Price, p.ImageUrl));

        return (products: productsDto, metaData: productsWithMetaData.MetaData);
    }

    public async Task<(IEnumerable<ProductDto>? products, MetaData metaData)> GetProductsForCategory(
        ProductParameters parameters, Guid categoryId, bool trackChanges)
    {
        if(!parameters.ValidPriceRange)
            throw new MaxPriceRangeBadRequestException();

        var category = await GetCategoryAndCheckIfExists(categoryId, trackChanges: false);
        {
            // la carga retrasada (lazy loading) necesita que se rastree la entidad de la bd por eso true
            var productsWithMetaData = await _repository.Product.GetProductsForCategory(parameters, categoryId, trackChanges: true); 

            var productsDto = productsWithMetaData.Select(
                p => new ProductDto(p.Id, p.Name, p.Description, p.Category.Name, p.Price, p.ImageUrl));

            return (products: productsDto, metaData: productsWithMetaData.MetaData);
        }

    }
    public async Task<ProductDto?> GetProductAsync(Guid id, bool trackChanges)
    {
        var productQuery = _repository.Product.GetProduct(id, trackChanges);

        var productDto = await productQuery
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Category.Name, p.Price, p.ImageUrl))
            .SingleOrDefaultAsync();

        return productDto;
    }

    public async Task<ProductDto> CreateProductAsync(ProductForCreationDto productFromRequest)
    {
        ICloudinaryHelper cloudinaryService = new CloudinaryHelper();
        // Como es creación y la verificación de la dirección url de la imagen se hace en el servicio cloudinary
        // una vez que no sea vacía o nula, entonces le paso una cadena vacía como lo que tendría que ser la imageurl
        var uploadResult = await cloudinaryService.UpsertPhotoAsync(productFromRequest.Image!, "");
        var productEntity = new Product
        {
            Active = true,
            CategoryId = productFromRequest.CategoryId,
            Description = productFromRequest.Description,
            ImageUrl = "https://res.cloudinary.com" + uploadResult.SecureUrl.AbsolutePath,
            Name = productFromRequest.Name
        };

        _repository.Product.CreateProduct(productEntity);
        var theCategory = await GetCategoryAndCheckIfExists(productFromRequest.CategoryId, trackChanges: false);
        await _repository.SaveAsync();

        var productToReturn = new ProductDto(
            productEntity.Id,
            productEntity.Name,
            productEntity.Description,
            theCategory.Name,
            productEntity.Price,
            productEntity.ImageUrl
            );
        return productToReturn;
    }

    public async Task DeleteProductAsync(Guid productId, bool trackChanges)
    {
        var product = await GetProductAndCheckIfExists(productId, trackChanges);

        _repository.Product.DeleteProduct(product);
        await _repository.SaveAsync();
    }

    public async Task UpdateProductAsync(Guid productId, ProductForUpdateDto productForUpdate, bool trackChanges)
    {
        var productFromDB = await GetProductAndCheckIfExists(productId, trackChanges);

        var category = await GetCategoryAndCheckIfExists(productForUpdate.CategoryId, trackChanges: false);

        _mapper.Map(productForUpdate, productFromDB);

        productFromDB.Active = true;

        await _repository.SaveAsync();
    }

    public async Task UpdateProductPictureAsync(Guid productId, IFormFile productPicture, bool trackChanges)
    {
        var productFromDB = await GetProductAndCheckIfExists(productId, trackChanges);

        ICloudinaryHelper cloudinaryService = new CloudinaryHelper();
        var uploadResult = await cloudinaryService.UpsertPhotoAsync(productPicture, productFromDB.ImageUrl);

        productFromDB.ImageUrl = "https://res.cloudinary.com" + uploadResult.SecureUrl.AbsolutePath;

        await _repository.SaveAsync();
    }

    private async Task<Category> GetCategoryAndCheckIfExists(Guid id, bool trackChanges)
    {
        return await _repository.Category.GetCategoryAsync(id, trackChanges: false)
                    ?? throw new CategoryNotFoundException(id);
    }
    private async Task<Product> GetProductAndCheckIfExists(Guid id, bool trackChanges)
    {
        return await _repository.Product.GetProduct(id, trackChanges: false).SingleOrDefaultAsync()
                    ?? throw new CategoryNotFoundException(id);
    }
}