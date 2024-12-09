using Grpc.Core;

namespace career_service.Services.Interfaces;

public interface ICareersService
{
    /// <summary>
    /// Metodo que retorna todas las carreras de la base de datos
    /// </summary>
    /// <returns>
    ///  Una Lista con todas las carreras en la base de datos
    /// </returns>
    public Task<CareerProto.CareersResponse> GetCareers(CareerProto.Empty request, ServerCallContext context);

    public Task<CareerProto.CareerResponse> GetById(CareerProto.CareerRequest request, ServerCallContext context);
}