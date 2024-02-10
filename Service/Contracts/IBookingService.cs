using System.Security.Claims;
using sdlt.DataTransferObjects;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Service.Contracts;

public interface IBookingService
{
    Task<(IEnumerable<BookingDto>? bookings, MetaData metaData)> GetAllBookingsAsync(BookingParameters bookingParameters, bool trackChanges);
    Task<BookingDto?> GetBookingAsync(Guid bookingId, bool trackChanges);
    Task<BookingDto> CreateAsync(BookingForCreationDto categoryForCreation, Guid eventId, string userId, bool trackChanges);
    // userId para verificar pertenencia de la reserva a cancelar a el user del controlador
    Task CancelBookingAsync(Guid bookingId, Guid userId, bool trackChanges);
    Task<IEnumerable<BookingDto>> GetMyBookings(ClaimsPrincipal me);
}