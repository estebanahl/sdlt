using AutoMapper;
using backEnd;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryForCreationDto>();
        CreateMap<Category, CategoryForCreationDto>().ReverseMap();
        CreateMap<Product, ProductForCreationDto>();
        CreateMap<Product, ProductForUpdateDto>();
        CreateMap<ProductForUpdateDto, Product>();
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<EventForCreationDto, Event>();   
        // CreateMap<EventForCreationDto, Event>();   
        CreateMap<Event, EventDto>()
            .ForMember(edto => edto.EventTypeName, opt => 
                    opt.MapFrom(e => e.EventType.Name));
        CreateMap<EventForUpdateDto, Event>();
        CreateMap<EventForUpdateDto, Event>().ReverseMap(); 
        CreateMap<Booking, BookingForCreationDto>();
        CreateMap<BookingForCreationDto, Booking>();
        CreateMap<BookingDto, Booking>();
        CreateMap<Booking, BookingDto>()
            .ForMember(bkdto => bkdto.UserName, opt =>
                opt.MapFrom(bk => bk.User.UserName))
            .ForMember(bkdto => bkdto.EventType, opt => 
                opt.MapFrom(bk => bk.Event.EventType.Name));
        CreateMap<User, UserWithRoles>();
        CreateMap<User, UserDto>();
            // .ForMember(udto => udto.Roles, opt =>
            //     opt.MapFrom(u => u.));
    }
}