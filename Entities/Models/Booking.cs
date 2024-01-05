using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sdlt.Entities.Models;

[Table("booking")]
public class Booking
{
    [Column("booking_id")]
    public Guid Id { get; set; }
    [Column("datetime")]
    public DateTime DateTime { get; set; }
    [Column("user_id")]
    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;
    [Column("active")]
    public bool Active { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual List<EventBooking> EventBooking{get;} = new();

}