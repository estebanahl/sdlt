using sdlt.DataTransferObjects;

namespace sdlt.Service.Contracts;
public interface IProductService { 
    Task<IEnumerable<ProductDto>?> GetAllProductsAsync(bool trackChanges);
    Task<ProductDto?> GetProductAsync(Guid productId, bool trackChanges);
    Task<ProductDto> CreateProductAsync(ProductForCreationDto productFromRequest);
}