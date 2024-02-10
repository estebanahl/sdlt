using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sdlt.ActionFilters;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AuthenticationController(IServiceManager service, UserManager<User> userManager, IMapper mapper)
    {
        _service = service;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var result = await _service.AuthenticationService.RegisterUser(userForRegistration);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        if (!await _service.AuthenticationService.ValidateUser(user))
            return Unauthorized();
        return Ok(new { Token = await _service.AuthenticationService.CreateToken() });
    }
    [HttpPost("roles")]
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleViewDto model)
    {
        IdentityResult result = await _service.AuthenticationService.CreateRole(model);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return StatusCode(201);
    }
    [HttpGet("users")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters userParameters)
    {
        var pagedResult = await _service.UserService.GetAllUsersAsync(userParameters, trackChanges: true);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        List<UserWithRoles> usersWithRoles = new();
        // tengo que hacer esto en el controlador porque aquí puedo usar el usermanager para obtener los roles
        foreach (User user in pagedResult.users!)
        {
            var userwithroles = _mapper.Map<UserWithRoles>(user);
            userwithroles.Roles = await _userManager.GetRolesAsync(user);
            usersWithRoles.Add(userwithroles);
        }
        return Ok(usersWithRoles);
    }
}