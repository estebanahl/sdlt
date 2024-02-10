using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sdlt.ActionFilters;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;

namespace sdlt.Controllers;

[Route("api/me")]
public class MeController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public MeController(IServiceManager service, UserManager<User> userManager, IMapper mapper)
    {
        _service = service;
        _userManager = userManager;
        _mapper = mapper;
    }


    [HttpGet]
    [Authorize(Roles = "User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMyself()
    {
        var me = User.Identity;
        // el middleware de exceptiones debería evitar no sea nulo y también lanzar el unauthorized. Por lo que si o si va a existir
        UserDto meDto = await _service.AuthenticationService.GetMyself(me!);
        return Ok(meDto);
    }
    [HttpGet("bookings")]
    [Authorize(Roles = "User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMyBookings(){
        var me = User;
        IEnumerable<BookingDto> myBookings = await _service.BookingService.GetMyBookings(me);
        return Ok(myBookings);
    }
}
