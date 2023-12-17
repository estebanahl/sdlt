using sdlt.Contracts;
using sdlt.Entities.Models;
using sdlt.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }

    public IQueryable<Product> GetAllProducts(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(p => p);

    public IQueryable<Product> GetProductAsync(Guid productId, bool trackChanges)=>
        FindByCondition(p => p.Id.Equals(productId), trackChanges);

    public void CreateProduct(Product product) => Create(product);
}