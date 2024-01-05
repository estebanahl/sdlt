using System.ComponentModel.DataAnnotations.Schema;

namespace sdlt.Entities.Models;

[Table("event_booking")]
public class EventBooking
{
    [Column("event_id")]
    [ForeignKey("Event")]
    public Guid EventId { get; set; }
    [Column("booking_id")]
    [ForeignKey("Booking")]
    public Guid BookingId { get; set; }
    public virtual Event Event { get; set; } = null!;
    public virtual Booking Booking { get; set; } = null!;
}