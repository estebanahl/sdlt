using sdlt.Entities.Exceptions;

namespace backEnd;

public class NoMoreRoomException : BadRequestException
{
    public NoMoreRoomException(Guid eventId, ushort seats, ushort currentQuota) 
        : base($"In the event of id {eventId}, there is room for {currentQuota} and you tried to book {seats} seats")
    {
    }
}
