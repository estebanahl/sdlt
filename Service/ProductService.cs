using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication.PgOutput.Messages;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
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
    public async Task<IEnumerable<ProductDto>?> GetAllProductsAsync(bool trackChanges)
    {
        var productsQuery = _repository.Product.GetAllProducts(trackChanges);
        var productsDto = await productsQuery
            .Include(p => p.Category)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Category.Name, p.ImageUrl))
            .ToListAsync();
        return productsDto;

    }

    public async Task<ProductDto?> GetProductAsync(Guid id, bool trackChanges)
    {
        var productQuery = _repository.Product.GetProductAsync(id, trackChanges);

        var productDto = await productQuery
            .Include(p => p.Category)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Category.Name, p.ImageUrl))
            .SingleOrDefaultAsync();

        return productDto;
    }

    public async Task<ProductDto> CreateProductAsync(ProductForCreationDto productFromRequest)
    {
        ICloudinaryHelper cloudinaryService = new CloudinaryHelper();
        // Como es creación y la verificación de la dirección url de la imagen se hace en el servicio cloudinary
        // una vez que no sea vacía o nula, entonces le paso una cadena vacía como lo que tendría que ser la imageurl
        var uploadResult = await cloudinaryService.UpsertPhotoAsync(productFromRequest.Image, "");
        var productEntity = new Product
        {
            Active = true,
            CategoryId = productFromRequest.CategoryId,
            Description = productFromRequest.Description,
            ImageUrl = "https://res.cloudinary.com" + uploadResult.SecureUrl.AbsolutePath,
            Name = productFromRequest.Name
        };
        _repository.Product.CreateProduct(productEntity);
        var theCategory = await _repository.Category.GetCategoryAsync(productFromRequest.CategoryId, trackChanges: false)
            ?? throw new CategoryNotFoundException(productFromRequest.CategoryId);
        _repository.SaveAsync();

        var productToReturn = new ProductDto(
            productEntity.Id,
            productEntity.Name,
            productEntity.Description,
            theCategory.Name,
            productEntity.ImageUrl
            );
        return productToReturn;
    }
}
