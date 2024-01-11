
using sdlt.Extensions;

namespace sdlt.Entities.RequestFeatures;

public class EventParameters : RequestParameters{
    public EventParameters() => OrderBy = "datetime";
    public DateOnly MinDate { get; set; } = DateOnly.Parse(Constants.FoundationDate);
    public DateOnly MaxDate { get; set; } = DateOnly.MaxValue;
    public bool ValidDateRange() => MinDate.CompareTo(MaxDate) <= 0;
    public string? SearchTerm { get; set; }
}