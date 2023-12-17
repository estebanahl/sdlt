using sdlt.Entities.Models;

namespace sdlt.Contracts
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllProducts(bool trackChanges);
        IQueryable<Product> GetProductAsync(Guid productId, bool trackChanges);
        void CreateProduct(Product product);
    }
}