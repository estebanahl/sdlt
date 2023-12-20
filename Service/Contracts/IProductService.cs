using sdlt.DataTransferObjects;
using sdlt.Entities.Models;

namespace sdlt.Service.Contracts;
public interface IProductService { 
    Task<IEnumerable<ProductDto>?> GetAllProductsAsync(bool trackChanges);
    Task<IEnumerable<ProductDto>?> GetProductsForCategory(Guid categoryId, bool trackChanges);
    Task<ProductDto?> GetProductAsync(Guid productId, bool trackChanges);
    Task<ProductDto> CreateProductAsync(ProductForCreationDto productFromRequest);
    Task DeleteProductAsync(Guid productId, bool trackChanges);
    Task UpdateProductAsync(Guid productId, ProductForUpdateDto productForUpdate, bool trackChanges);
    Task UpdateProductPictureAsync(Guid productId, IFormFile productPicture, bool trackChanges);
}