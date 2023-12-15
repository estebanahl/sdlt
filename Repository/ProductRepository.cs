using sdlt.Contracts;
using sdlt.Entities.Models;
using sdlt.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }
}