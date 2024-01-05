namespace sdlt.Entities.RequestFeatures;

public class EventParameters : RequestParameters{
    public EventParameters() => OrderBy = "datetime";
    public DateTime MinDate { get; set; }
    public DateTime MaxDate { get; set; }

    public string? SearchTerm { get; set; }
}