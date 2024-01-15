
using sdlt.Extensions;

namespace sdlt.Entities.RequestFeatures;

public class BookingParameters : RequestParameters
{
    public BookingParameters() => OrderBy = "StartTime";
    public TimeOnly MinHour { get; set; } = TimeOnly.Parse(Constants.HourLastStartBooking);
    public TimeOnly MaxHour { get; set; } = TimeOnly.Parse(Constants.HourOfEndOfWork);
    public Guid EventId { get; set; } = Guid.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public bool ValidDateRange() => MinHour.CompareTo(MaxHour) <= 0;
    public string? SearchTerm { get; set; }
}
