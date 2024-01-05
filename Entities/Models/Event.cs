using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sdlt.Entities.Models;

[Table("event")]
public class Event{
    [Column("event_id")]
    public Guid Id { get; set; }
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    public DateOnly EventDate {get; set;}
    [Column("description")]
    public string Description { get; set; } = string.Empty;    
    [Column("quota")]
    public ushort Quota { get; set; }    
    [Column("active")]
    public bool Active { get; set; }
    public virtual List<EventBooking> EventBooking{get;} = new();
}