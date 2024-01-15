using System.Drawing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Service.Contracts;

namespace sdlt.Service;

public class EventService : IEventService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public EventService(IRepositoryManager repository, ILoggerManager
   logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<EventDto> CreateEventAsync(EventForCreationDto eventForCreation)
    {
        var eventTrackedEntity = _mapper.Map<Event>(eventForCreation);
        eventTrackedEntity.Active = true;
        _repository.Event.CreateEvent(eventTrackedEntity);
        await _repository.SaveAsync();
        return _mapper.Map<EventDto>(eventTrackedEntity);
    }

    public async Task<(IEnumerable<EventDto>? events, MetaData metaData)> GetAllEventsAsync(EventParameters parameters, bool trackChanges)
    {
        if (!parameters.ValidDateRange())
            throw new MaxDateRangeBadRequestException();

        var eventsWithMetadata = await _repository.Event.GetAllEvents(parameters, trackChanges);
        IEnumerable<EventDto> events = _mapper.Map<IEnumerable<EventDto>>(eventsWithMetadata);

        return (events: events, metaData: eventsWithMetadata.MetaData);
    }

    
    public async Task<EventDto> GetEventAsync(Guid id, bool trackChanges)
    {
        Event? eventEntity = await _repository.Event.GetEvent(id, trackChanges).SingleOrDefaultAsync()
            ?? throw new EventNotFoundException(id);
        return _mapper.Map<EventDto>(eventEntity);
    }

    public async Task<(EventForUpdateDto eventToPatch, Event eventEntity)> GetEventForPatch(
        Guid eventId, bool trackChanges)
    {
        var eventEntity = await _repository.Event.GetEvent(eventId, trackChanges)
                .SingleOrDefaultAsync()
                    ?? throw new EventNotFoundException(eventId);
        var eventToPatch = _mapper.Map<EventForUpdateDto>(eventEntity);

        return (eventToPatch, eventEntity);
    }

    public async Task SaveChangesToPatch(EventForUpdateDto eventToPatch, Event eventEntity)
    {
        _mapper.Map(eventToPatch, eventEntity);
        await _repository.SaveAsync();
    }
}
