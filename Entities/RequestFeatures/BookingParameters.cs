
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sdlt.Extensions;

namespace sdlt.Entities.RequestFeatures;

public class BookingParameters : RequestParameters
{

    public BookingParameters() => OrderBy = "StartTime";
    public int MinHour { get; set; }
    public int MinMinute { get; set; }
    public int MaxHour { get; set; } = Constants.HourOfEndOfWork;
    public int MaxMinute { get; set; } = Constants.MinuteOfEndOfWork;
    public Guid EventId { get; set; } = Guid.Empty;
    public Guid UserId { get; set; } = Guid.Empty;
    public bool ValidDateRange()
    {
        var maxTime = new TimeOnly(MaxHour, MaxMinute);
        var minTime = new TimeOnly(MinHour, MinMinute);
        
        return minTime.CompareTo(maxTime) <= 0;
    }

    public string? SearchTerm { get; set; }
}
