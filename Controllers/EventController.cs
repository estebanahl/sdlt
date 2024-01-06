using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using sdlt.ActionFilters;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Controllers;

[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IServiceManager _service;

    public EventController(IServiceManager service) => _service = service;

    [HttpPost]
    // [Authorize]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Post([FromBody] EventForCreationDto eventForCreation)
    {
        EventDto theEvent = await _service.EventService.CreateEventAsync(eventForCreation);
        return Ok(theEvent);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] EventParameters parameters)
    {
        var pagedResult = await _service.EventService.GetAllEventsAsync(parameters, trackChanges: false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.events);
    }
    [HttpPatch("{id:guid}")]
    // [Authorize]
    // [ServiceFilter(typeof(ValidationFilterAttribute))] // la validación de objeto nulo hace que no funcione
    // porque el dichoso patchdoc quiere que se escriba como array (empezando con [] y dentro las llaves)
    // esto detona el objeto nulo por alguna razón
    public async Task<IActionResult> PatchEvent(Guid id, [FromBody]JsonPatchDocument<EventForUpdateDto> patchDoc){
        var result = await _service.EventService.GetEventForPatch(id, trackChanges: true);
        patchDoc.ApplyTo(result.eventToPatch);
        await _service.EventService.SaveChangesToPatch(result.eventToPatch, result.eventEntity);

        return NoContent();
    }
    [HttpGet("{id:guid}", Name = "EventById")]
    // [Authorize]
    public async Task<IActionResult> Get(Guid id){
        EventDto eventDto = await _service.EventService.GetEventAsync(id, trackChanges: false);

        return Ok(eventDto);
    }
}
