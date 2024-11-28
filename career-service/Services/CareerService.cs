using career_service.DTOs;
using career_service.Services.Interfaces;

namespace career_service.Services;

public class CareerService : ICareersService
{
    public Task<List<CareerDto>> GetAll()
    {
        throw new NotImplementedException();
    }
}