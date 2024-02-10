using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Repository;
using sdlt.Repository.Extensions;

namespace backEnd;

public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }
    public void CreateBookInEvent(Booking booking)
    {
        Create(booking);
    }

    public void DeleteBooking(Booking booking)
    {
        Delete(booking);
    }

    public async Task<Booking?> GetBooking(Guid bookingId, bool trackChanges)
    {
        return await FindByCondition(b => b.Id.Equals(bookingId), trackChanges).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Booking>> GetBookingByUserId(string userId, bool trackChanges)
    {
        return await FindByCondition(b => b.UserId.Equals(userId), trackChanges).ToListAsync();
    }

    public async Task<PagedList<Booking>> GetBookings(BookingParameters bookingParameters, bool trackChanges)
    {
        var query = FindAll(trackChanges)
                .FilterByUser(bookingParameters.UserId)
                .FilterByEvent(bookingParameters.EventId)
                .FilterHourOfBookings(bookingParameters.MinHour, bookingParameters.MinMinute, bookingParameters.MaxHour, bookingParameters.MaxMinute)
                .SearchByUserName(bookingParameters.SearchTerm!)
                .Sort(bookingParameters.OrderBy!)
                .Skip((bookingParameters.PageNumber - 1) * bookingParameters.PageSize)
                .Take(bookingParameters.PageSize);


        var count = await query.CountAsync();
        List<Booking> bookingsList = await query.ToListAsync();
        return PagedList<Booking>.ToPagedList(bookingsList, count, bookingParameters.PageNumber, bookingParameters.PageSize);

    }
}
