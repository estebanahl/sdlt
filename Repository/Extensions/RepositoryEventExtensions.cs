
using sdlt.Entities.Models;
using System.Linq.Dynamic.Core;
using sdlt.Repository.Extensions.Utility;
using Microsoft.EntityFrameworkCore;

namespace sdlt.Repository.Extensions;

public static class RepositoryEventExtensions
{
    public static IQueryable<Event> FilterEvents(
        this IQueryable<Event> events, DateOnly minDate, DateOnly maxDate) =>
            events.Where(e => e.StartDate.CompareTo(minDate) >= 0 || e.EndDate.CompareTo(maxDate) <= 0);// cuidado con el comienzo y fin de fechas

    public static IQueryable<Event> Search(this IQueryable<Event> events, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return events;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return events
            .Include(e => e.EventType)
            .Where(p => p.EventType.Name.ToLower().Contains(lowerCaseTerm));
    }
    public static IQueryable<Event> Sort(this IQueryable<Event> events, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return events
                .OrderBy(p => p.StartDate);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Event>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return events
                .Include(e => e.EventType)
                .OrderBy(p => p.EventType.Name);

        return events.OrderBy(orderQuery);
    }
}