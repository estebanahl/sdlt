
namespace sdlt.Entities.Exceptions;

public sealed class BookingNotFoundException : NotFoundException
{
    public BookingNotFoundException(Guid bookingId) :
        base($"The booking with id: {bookingId} doesn't exist in the database.")
    { }
}
