using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;

namespace sdlt.Contracts
{
    public interface IEventRepository
    {
        Task<PagedList<Event>> GetAllEvents(EventParameters eventParameters, bool trackChantes);
        IQueryable<Event> GetEvent(Guid eventId, bool trackChantes);
        void CreateEvent(Event theevent);
    }
}