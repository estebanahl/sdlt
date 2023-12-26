using AutoMapper;
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
    }
}