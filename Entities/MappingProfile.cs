using AutoMapper;
using sdlt.DataTransferObjects;
using sdlt.Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}