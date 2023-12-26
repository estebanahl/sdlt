using sdlt.DataTransferObjects;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Service.Contracts;
public interface IProductService { 
    Task<(IEnumerable<ProductDto>? products, MetaData metaData)> GetAllProductsAsync(
        ProductParameters parameters, bool trackChanges);
    Task<(IEnumerable<ProductDto>? products, MetaData metaData)> GetProductsForCategory(
        ProductParameters parameters, Guid categoryId, bool trackChanges);
    Task<ProductDto?> GetProductAsync(Guid productId, bool trackChanges);
    Task<ProductDto> CreateProductAsync(ProductForCreationDto productFromRequest);
    Task DeleteProductAsync(Guid productId, bool trackChanges);
    Task UpdateProductAsync(Guid productId, ProductForUpdateDto productForUpdate, bool trackChanges);
    Task UpdateProductPictureAsync(Guid productId, IFormFile productPicture, bool trackChanges);
}