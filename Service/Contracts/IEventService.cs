using sdlt.DataTransferObjects;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Service.Contracts;
public interface IEventService { 
    Task<(IEnumerable<EventDto>? events, MetaData metaData)> GetAllEventsAsync(
        EventsParameters parameters, bool trackChanges);
    Task CreateEvent(EventForCreationDto theEvent);
    Task UpdateEvent(Guid eventId, EventForUpdateDto theEvent, bool trackChanges);
    Task UpdateEventQuota(Guid eventId, EventForQuotaUpdateDto theEvent);
    
}