using System.Reflection.Metadata.Ecma335;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Service.Contracts;

public interface IBookingService
{
    Task<(IEnumerable<BookingDto>? bookings, MetaData metaData)> GetAllBookingsAsync(BookingParameters bookingParameters, bool trackChanges);
    Task<BookingDto?> GetBookingAsync(Guid bookingId, bool trackChanges);
    Task<BookingDto> CreateAsync(BookingForCreationDto categoryForCreation, Guid eventId, string userId, bool trackChanges);
}