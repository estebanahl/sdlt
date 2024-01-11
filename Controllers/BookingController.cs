using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;

namespace sdlt.Controllers;

[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<BookingController> _logger;
    public BookingController(
        IServiceManager service,
        UserManager<User> userManager,
        ILogger<BookingController> logger)
    {
        _service = service;
        _userManager = userManager;
        _logger = logger;
    }

    [HttpPost("{eventId:guid}/event")]
    [Authorize]
    public async Task<IActionResult> Post(BookingForCreationDto bookingForCreationDto, Guid eventId){

    }
}
