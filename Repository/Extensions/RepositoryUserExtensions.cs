using sdlt.Entities.Models;
using sdlt.Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace sdlt.Repository.Extensions;

public static class RepositoryUserExtensions
{
  public static IQueryable<User> Sort(this IQueryable<User> users, string orderByQueryString)
    {
        var orderQuery = OrderQueryBuilder.CreateOrderQuery<User>(orderByQueryString);

        return users.OrderBy(orderQuery);
    }
}

