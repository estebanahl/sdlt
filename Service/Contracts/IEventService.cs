using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Service.Contracts;
public interface IEventService { 
    Task<(IEnumerable<EventDto>? events, MetaData metaData)> GetAllEventsAsync(
        EventParameters parameters, bool trackChanges);
    Task<EventDto> GetEventAsync(Guid id, bool trackChanges);
    Task<EventDto> CreateEventAsync(EventForCreationDto eventForCreation);
    // con el siguiente se cubren los casos de (re)establecimiento del cupo de eventos y nuevas fechas
    Task<(EventForUpdateDto eventToPatch, Event eventEntity)> GetEventForPatch(
        Guid eventId, bool trackChanges);
    Task SaveChangesToPatch(EventForUpdateDto eventToPatch, Event eventEntity);
    
    
}