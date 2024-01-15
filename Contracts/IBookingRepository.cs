using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Contracts;

public interface IBookingRepository
{
    Task<PagedList<Booking>> GetBookings(BookingParameters bookingParameters, bool trackChanges);
    Task<Booking?> GetBooking(Guid bookingId, bool trackChanges);
    void CreateBookInEvent(Booking booking);
}
