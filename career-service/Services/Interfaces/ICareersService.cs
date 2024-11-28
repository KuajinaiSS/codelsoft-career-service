using career_service.DTOs;

namespace career_service.Services.Interfaces;

public interface ICareersService
{
    /// <summary>
    /// Metodo que retorna todas las carreras de la base de datos
    /// </summary>
    /// <returns>
    ///  Una Lista con todas las carreras en la base de datos <see cref="CareerDto"/>
    /// </returns>
    public Task<List<CareerDto>> GetAll();
}