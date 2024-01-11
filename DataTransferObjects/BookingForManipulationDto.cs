using System.ComponentModel.DataAnnotations;
using sdlt.Entities.Exceptions;
using sdlt.Extensions;

namespace sdlt.DataTransferObjects;

public record BookingForManipulationDto
{
    [Required]
    public Guid EventId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    private TimeOnly _startTime;
    [Required]
    public TimeOnly StartTime
    {
        get
        {
            return _startTime;
        }
        set
        {
            if (value.CompareTo(DateOnly.Parse(Constants.HourLastStartBooking)) <= 0)
            {
                _startTime = value;
            }
            else
            {
                throw new TooLateBookingException();
            }
        }
    }
    
}
