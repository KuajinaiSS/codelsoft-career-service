using career_service.DTOs;
using career_service.Repositories.Interfaces;
using career_service.Services.Interfaces;

namespace career_service.Services;

public class CareerService : ICareersService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapperService;
    public Task<List<CareerDto>> GetAll()
    {
        throw new NotImplementedException();
    }
}