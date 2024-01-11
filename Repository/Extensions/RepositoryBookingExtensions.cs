
using sdlt.Entities.Models;
using System.Linq.Dynamic.Core;
using sdlt.Repository.Extensions.Utility;

namespace sdlt.Repository.Extensions;

public static class RepositoryBookingExtensions{
    public static IQueryable<Booking> FilterHourOfBookings(
        this IQueryable<Booking> bookings, TimeOnly minHour, TimeOnly maxHour) =>
            bookings.Where(e => e.StartTime.CompareTo(minHour) >= 0 || e.StartTime.CompareTo(maxHour) <= 0);
    
    // public static IQueryable<Event> Search(this IQueryable<Event> events, string searchTerm){
    //     if(string.IsNullOrWhiteSpace(searchTerm))
    //         return events;

    //     var lowerCaseTerm = searchTerm.Trim().ToLower();

    //     return events.Where(p => p.Name.ToLower().Contains(lowerCaseTerm));
    // }
    // public static IQueryable<Event> Sort(this IQueryable<Event> events, string orderByQueryString){
    //     if(string.IsNullOrWhiteSpace(orderByQueryString))
    //         return events.OrderBy(p => p.EventDate);
        
    //     var orderQuery = OrderQueryBuilder.CreateOrderQuery<Event>(orderByQueryString);

    //     if(string.IsNullOrWhiteSpace(orderQuery))
    //         return events.OrderBy(p => p.Name);

    //     return events.OrderBy(orderQuery);
    // }
}
