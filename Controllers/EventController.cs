using System.Security.Claims;
using System.Text.Json;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sdlt.ActionFilters;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Controllers;

// como en los gets "hacemos joins" también el lazy loader de la entidad anidada necesita que trackChanges sea true
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly UserManager<User> _userManager;

    public EventController(IServiceManager service, UserManager<User> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    [HttpPost]
    // [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Post([FromBody] EventForCreationDto eventForCreation)
    {
        EventDto theEvent = await _service.EventService.CreateEventAsync(eventForCreation);
        return CreatedAtRoute("EventById", new { id = theEvent.Id }, theEvent);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] EventParameters parameters)
    {

        var pagedResult = await _service.EventService.GetAllEventsAsync(parameters, trackChanges: true);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.events);
    }
    [HttpPatch("{id:guid}")]
    // [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> PatchEvent(Guid id, [FromBody] JsonPatchDocument<EventForUpdateDto> patchDoc)
    {
        var result = await _service.EventService.GetEventForPatch(id, trackChanges: true);
        patchDoc.ApplyTo(result.eventToPatch);
        await _service.EventService.SaveChangesToPatch(result.eventToPatch, result.eventEntity);

        return NoContent();
    }
    [HttpGet("{id:guid}", Name = "EventById")]
    // [Authorize]    
    public async Task<IActionResult> Get(Guid id)
    {
        EventDto eventDto = await _service.EventService.GetEventAsync(id, trackChanges: true);

        return Ok(eventDto);
    }
    [HttpPost("{eventId:guid}/booking")]
    [Authorize(Roles = "User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]   
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Post([FromBody] BookingForCreationDto bookingForCreationDto, Guid eventId)
    {
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var theBooking = await _service.BookingService.CreateAsync(bookingForCreationDto, eventId, userId, trackChanges: true);
        return CreatedAtAction(nameof(Get), new { controller = "Booking", id = theBooking.Id }, theBooking);
    }
}
