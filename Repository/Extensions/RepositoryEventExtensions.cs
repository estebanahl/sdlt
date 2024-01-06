
using sdlt.Entities.Models;
using System.Linq.Dynamic.Core;
using sdlt.Repository.Extensions.Utility;

namespace sdlt.Repository.Extensions;

public static class RepositoryEventExtensions{
    public static IQueryable<Event> FilterEvents(
        this IQueryable<Event> events, DateOnly minDate, DateOnly maxDate) =>
            events.Where(e => e.EventDate.CompareTo(minDate) >= 0 || e.EventDate.CompareTo(maxDate) <= 0);
    
    public static IQueryable<Event> Search(this IQueryable<Event> events, string searchTerm){
        if(string.IsNullOrWhiteSpace(searchTerm))
            return events;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return events.Where(p => p.Name.ToLower().Contains(lowerCaseTerm));
    }
    public static IQueryable<Event> Sort(this IQueryable<Event> events, string orderByQueryString){
        if(string.IsNullOrWhiteSpace(orderByQueryString))
            return events.OrderBy(p => p.EventDate);
        
        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Event>(orderByQueryString);

        if(string.IsNullOrWhiteSpace(orderQuery))
            return events.OrderBy(p => p.Name);

        return events.OrderBy(orderQuery);
    }
}