using sdlt.Extensions;

namespace sdlt.DataTransferObjects;

public record EventDto
{
    public Guid Id {get;set;}
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public ushort Quota { get; set; }
    public Guid EventTypeId { get; set; }
    public string EventTypeName { get; set; } = string.Empty;
}