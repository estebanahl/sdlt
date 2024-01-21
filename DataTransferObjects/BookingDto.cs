using Castle.Core.Internal;

namespace sdlt.DataTransferObjects;

public record class BookingDto
{
    public Guid Id { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public bool Active { get; set; }
    public ushort Seats { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
}