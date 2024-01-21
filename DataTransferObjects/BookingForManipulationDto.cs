using System.ComponentModel.DataAnnotations;
using sdlt.Entities.Exceptions;
using sdlt.Extensions;

namespace sdlt.DataTransferObjects;

public record BookingForManipulationDto
{

    // public Guid UserId { get; set; } // el user viene dado por añadidura como dice la biblia con el authorize en el controller
    [Range(1, Constants.MaximumSeatsPerUser)]
    public int Seats { get; set; }
    private TimeOnly _arrivalTime;
    [Required]
    public TimeOnly ArrivalTime
    {
        get
        {
            return _arrivalTime;
        }
        set
        {
            if (value.CompareTo(new TimeOnly(Constants.HourLastStartBooking, Constants.MinuteLastStartBooking)) <= 0)
            {
                _arrivalTime = value;
            }
            else
            {
                throw new TooLateBookingException();
            }
        }
    }

}
