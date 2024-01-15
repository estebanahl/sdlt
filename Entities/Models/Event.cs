using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sdlt.Entities.Models;

[Table("event")]
public class Event
{
    [Column("event_id")]
    public Guid Id { get; set; }
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    [Column("event_start_date")]
    public DateOnly StartDate { get; set; }
    [Column("event_end_date")]
    public DateOnly EndDate { get; set; }
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    [Column("quota")]
    public ushort Quota { get; set; }
    [Column("active")]
    public bool Active { get; set; }
     [Column("event_type")]
    [ForeignKey("EventType")]
    public Guid EventTypeId { get; set; }
    public virtual EventType EventType { get; set; } = null!;
    public virtual IEnumerable<Booking> Bookings{get;set;} = new List<Booking>();
}