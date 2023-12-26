using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Repository;
using sdlt.Repository.Extensions;
namespace sdlt.Repository;
public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }

    public async Task<PagedList<Product>> GetAllProducts(ProductParameters parameters, bool trackChanges){
        var query = FindByCondition(p => p.Price >= parameters.MinPrice && p.Price <= parameters.MaxPrice, trackChanges)
            .FilterProducts(parameters.MinPrice, parameters.MaxPrice)
            .Search(parameters.SearchTerm!)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Sort(parameters.OrderBy!);

        var count = await query.CountAsync();

        var productsPagedList = await query.ToListAsync();

        return PagedList<Product>.ToPagedList(productsPagedList, count, parameters.PageNumber, parameters.PageSize);
    }
  

    public IQueryable<Product> GetProduct(Guid productId, bool trackChanges) =>
        FindByCondition(p => p.Id.Equals(productId), trackChanges);

    public void CreateProduct(Product product) => Create(product);
    public void DeleteProduct(Product product) => Delete(product);

    public async Task<PagedList<Product>> GetProductsForCategory(ProductParameters parameters, Guid categoryId, bool trackChanges) {
        var query = FindByCondition(p => p.CategoryId.Equals(categoryId) && p.Price >= parameters.MinPrice && p.Price <= parameters.MaxPrice, trackChanges)
            .FilterProducts(parameters.MinPrice, parameters.MaxPrice)
            .Search(parameters.SearchTerm!)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Sort(parameters.OrderBy!);
            
        var count = await query.CountAsync();

        var productsList = await query.ToListAsync();
        return PagedList<Product>.ToPagedList(productsList, count, parameters.PageNumber, parameters.PageSize);
    }
       
}