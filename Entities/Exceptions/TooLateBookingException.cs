using sdlt.Entities.Exceptions;

namespace sdlt.Entities.Exceptions;

public class TooLateBookingException : BadRequestException
{
    public TooLateBookingException()
        : base("Unable to book the user with less than 2 hours before close")
    {
    }
}
