using AutoMapper;
using career_service.DTOs;
using career_service.Models;

namespace career_service.Extensions;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        CreateMap<Career, CareerDto>();
    }
    
}