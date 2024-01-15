
using sdlt.Entities.Models;
using System.Linq.Dynamic.Core;
using sdlt.Repository.Extensions.Utility;
using Microsoft.EntityFrameworkCore;

namespace sdlt.Repository.Extensions;

public static class RepositoryBookingExtensions
{
    public static IQueryable<Booking> FilterHourOfBookings(
        this IQueryable<Booking> bookings, TimeOnly minHour, TimeOnly maxHour) =>
            bookings.Where(e => e.ArrivalTime.CompareTo(minHour) >= 0 || e.ArrivalTime.CompareTo(maxHour) <= 0);

    public static IQueryable<Booking> FilterByEvent(
        this IQueryable<Booking> bookings, Guid eventId)
    {
        if (!eventId.Equals(Guid.Empty))
            return bookings
                .Where(b => b.Event.Id.Equals(eventId));
        else
            return bookings;
    }
    public static IQueryable<Booking> FilterByUser(
        this IQueryable<Booking> bookings, Guid userId) // puede hacerse por username también
    {
        if (!userId.Equals(Guid.Empty))
            return bookings
                .Include(b => b.User)
                .Where(b => b.User.Id.Equals(userId));
        else
            return bookings;
    }
    public static IQueryable<Booking> SearchByUserName(this IQueryable<Booking> events, string searchTerm){
        if(string.IsNullOrWhiteSpace(searchTerm))
            return events;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return events
            .Include(b => b.User)
            .Where(b => b.User.UserName.ToLower().Contains(lowerCaseTerm));
    }
    public static IQueryable<Booking> Sort(this IQueryable<Booking> events, string orderByQueryString){
        if(string.IsNullOrWhiteSpace(orderByQueryString))
            return events.OrderBy(b => b.ArrivalTime);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Booking>(orderByQueryString);

        if(string.IsNullOrWhiteSpace(orderQuery))
            return events.OrderBy(b => b.ArrivalTime);

        return events.OrderBy(orderQuery);
    }
}
