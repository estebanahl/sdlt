using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Contracts;

public interface IBookingRepository
{
    Task<PagedList<Booking>> GetBookings(BookingParameters bookingParameters, bool trackChanges);
    void BookInEvent(BookingForCreationDto bookingForCreationDto, Guid EventId);
}
