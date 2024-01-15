using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Repository.Extensions;

namespace sdlt.Repository;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public void CreateEvent(Event theevent)
    {
        Create(theevent);
    }

    public async Task<PagedList<Event>> GetAllEvents(EventParameters eventParameters, bool trackChanges)
    {
        var query = FindAll(trackChanges)
             .FilterEvents(eventParameters.MinDate, eventParameters.MaxDate)
             .Search(eventParameters.SearchTerm!)
             .Sort(eventParameters.OrderBy!)
             .Skip((eventParameters.PageNumber - 1) * eventParameters.PageSize)
             .Take(eventParameters.PageSize);
             

        var count = await query.CountAsync();

        var eventsPagedList = await query.ToListAsync();
        return PagedList<Event>.ToPagedList(eventsPagedList, count, eventParameters.PageNumber, eventParameters.PageSize);
    }

    public IQueryable<Event> GetEvent(Guid eventId, bool trackChanges)
    {
        return FindByCondition(e => e.Id.Equals(eventId), trackChanges);
    }
}