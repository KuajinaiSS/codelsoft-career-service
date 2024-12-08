using career_service.DTOs;
using career_service.Repositories.Interfaces;
using career_service.Services.Interfaces;
using CareerProto;
using Grpc.Core;

namespace career_service.Services;

public class CareerService : CareerProto.CareerService.CareerServiceBase , ICareersService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapperService;
    
    public CareerService(IUnitOfWork unitOfWork, IMapperService mapperService)
    {
        _unitOfWork = unitOfWork;
        _mapperService = mapperService;
    }

    public async Task<CareerProto.CareerResponse> GetById(CareerRequest request, ServerCallContext context)
    {
        //var career = await _unitOfWork.CareersRepository.GetByiD(request.Id);

        //var response = new CareerProto.CareerResponse
        //{
            //Career = career ?? new CareerProto.Career()
        //};
        
        //return await Task.FromResult(response);
        
        throw new NotImplementedException();
    }

    public override async Task<CareerProto.CareersResponse> GetCareers(CareerProto.Empty request, ServerCallContext context)
    {
        Console.WriteLine("In GetAll Careers Request");
        
        var response = new CareerProto.CareersResponse();
        Console.WriteLine("Created List Request");
        
        Console.WriteLine("Initialization career Repository");
        var careers = await _unitOfWork.CareersRepository.Get();
        Console.WriteLine("Finish career Repository " + careers);
        
        response.Careers.AddRange(careers.Select(c => new CareerProto.Career
        {
            Id = c.Id,
            Name = c.Name,
        }));

        return await Task.FromResult(response);
    }
}