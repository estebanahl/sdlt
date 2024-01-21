using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

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

    [HttpGet("{id:guid}")]
    [ActionName(nameof(Get))]
    public async Task<IActionResult> Get(Guid id){
        var bookingDto = await _service.BookingService.GetBookingAsync(id, trackChanges: true);
        return Ok(bookingDto);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(BookingParameters bookingParameters){
        var pagedResult = await _service.BookingService.GetAllBookingsAsync(bookingParameters, trackChanges: true);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.bookings);
    }
}
