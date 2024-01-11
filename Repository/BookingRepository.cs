using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Repository;
using sdlt.Repository.Extensions;

namespace backEnd;

public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    {
    }
    public void BookInEvent(BookingForCreationDto bookingForCreationDto, Guid EventId)
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<Booking>> GetBookings(BookingParameters bookingParameters, bool trackChanges)
    {
        var query = FindAll(trackChanges).
            FilterHourOfBookings(bookingParameters.MinHour, bookingParameters.MaxHour);
            // falta agrupar y filtrar por evento
    
    }
}
