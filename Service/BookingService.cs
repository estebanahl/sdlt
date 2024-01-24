using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sdlt.Contracts;
using sdlt.DataTransferObjects;
using sdlt.Entities.Exceptions;
using sdlt.Entities.Models;
using sdlt.Entities.RequestFeatures;
using sdlt.Service.Contracts;

namespace sdlt.Service;

public class BookingService : IBookingService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public BookingService(IRepositoryManager repository, ILoggerManager
    logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BookingDto> CreateAsync(BookingForCreationDto bookingForCreation, Guid eventId, string userId, bool trackChanges)
    {
        // mapeo del dto cuyos atributos forman un subconjunto propio del Booking en sí a un objecto booking, este se rastrea y se llenará su campo id al crearse no se cómo pero está ahí
        var bookingTrackedEntity = _mapper.Map<Booking>(bookingForCreation);
        // este campo representaría las validaciones o desactivaciones más complejas que se tendrían que hacer en el mundo de verdad
        bookingTrackedEntity.Active = true;
        bookingTrackedEntity.UserId = userId;
        bookingTrackedEntity.EventId = eventId;
        // primero se valida si hay espacio
        await _repository.Event.UpdateAndControlQuota(eventId,(ushort) bookingForCreation.Seats);
        _repository.Booking.CreateBookInEvent(bookingTrackedEntity);
        
        await _repository.SaveAsync();

        return _mapper.Map<BookingDto>(bookingTrackedEntity); 
    }
    public async Task<(IEnumerable<BookingDto>? bookings, MetaData metaData)>
        GetAllBookingsAsync(BookingParameters bookingParameters, bool trackChanges)
    {
        if (!bookingParameters.ValidDateRange())
            throw new MaxDateRangeBadRequestException();

        var bookingsWithMetaData = await _repository.Booking.GetBookings(bookingParameters, trackChanges);
        IEnumerable<BookingDto>? bookings = _mapper.Map<IEnumerable<BookingDto>>(bookingsWithMetaData);
        return (bookings: bookings, metaData: bookingsWithMetaData.MetaData);
    }

    public async Task<BookingDto?> GetBookingAsync(Guid bookingId, bool trackChanges)
    {
        Booking? bookingEntity = await _repository.Booking.GetBooking(bookingId, trackChanges) 
            ?? throw new BookingNotFoundException(bookingId);
        
        return _mapper.Map<BookingDto>(bookingEntity);

    }
    public async Task CancelBookingAsync(Guid bookingId, Guid userIdfromController, bool trackChanges)
    {
        // se obtiene el booking que se quiere eliminar (sin monitoreo (trackChanges=false) para mayor velocidad)
        Booking? bookingEntity = await _repository.Booking.GetBooking(bookingId, trackChanges)
            ?? throw new BookingNotFoundException(bookingId);
        // user Id en la bd porque así lo dispuso identity es un string, entonces se eligió convertir el guid del controller a string
        if(!bookingEntity.UserId.Equals(userIdfromController.ToString()))
            throw new Exception("You can try but actually you can't delete booking of another person"); // nunca probé

        Event? eventEntity = await _repository.Event.GetEvent(bookingEntity.EventId, trackChanges: true)
            .SingleOrDefaultAsync()
                ?? throw new EventNotFoundException(bookingEntity.EventId);

        // revertimos o sumamos de vuelta a la cantidad disponible, como está rastreada la entidad solo con editar y luego 
        // guardar es suficiente
        eventEntity.Quota += (ushort)bookingEntity.Seats;
        _repository.Booking.DeleteBooking(bookingEntity);
        
        await _repository.SaveAsync();
    }
}
