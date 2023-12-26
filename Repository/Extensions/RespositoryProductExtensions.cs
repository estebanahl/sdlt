using System.Text;
using sdlt.Entities.Models;
using System.Linq.Dynamic.Core;
using sdlt.Repository.Extensions.Utility;

namespace sdlt.Repository.Extensions;

public static class REpositoryProductExtensions{
    public static IQueryable<Product> FilterProducts(
        this IQueryable<Product> products, decimal minPrice, decimal maxPrice) =>
            products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
    
    public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm){
        if(string.IsNullOrWhiteSpace(searchTerm))
            return products;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return products.Where(p => p.Name.ToLower().Contains(lowerCaseTerm));
    }
    public static IQueryable<Product> Sort(this IQueryable<Product> products, string orderByQueryString){
        if(string.IsNullOrWhiteSpace(orderByQueryString))
            return products.OrderBy(p => p.Name);
        
        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);

        if(string.IsNullOrWhiteSpace(orderQuery))
            return products.OrderBy(p => p.Name);

        return products.OrderBy(orderQuery);
    }
}