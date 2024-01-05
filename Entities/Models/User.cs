using Microsoft.AspNetCore.Identity;

namespace sdlt.Entities.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<Booking> Bookings { get; set; } = new(); //cuidado
}