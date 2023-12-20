using sdlt.Entities.Models;

namespace sdlt.Contracts
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllProducts(bool trackChanges);
        IQueryable<Product> GetProductsForCategory(Guid categoryId, bool trackChanges);
        IQueryable<Product> GetProduct(Guid productId, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}