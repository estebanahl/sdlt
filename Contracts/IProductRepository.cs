
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Contracts
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAllProducts(ProductParameters parameters, bool trackChanges);
        Task<PagedList<Product>> GetProductsForCategory(ProductParameters parameters, Guid categoryId, bool trackChanges);
        IQueryable<Product> GetProduct(Guid productId, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}