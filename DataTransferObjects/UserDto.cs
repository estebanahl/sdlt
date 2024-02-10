using sdlt.Entities.Models;

namespace sdlt.DataTransferObjects;

public record UserDto
{
public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    // public List<Booking> Bookings { get; set; } = null!;
}
