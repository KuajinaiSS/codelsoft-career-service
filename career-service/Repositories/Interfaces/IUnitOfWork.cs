namespace career_service.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the careers repository.
    /// </summary>
    /// <value>A Concrete class for ICareersRepository</value>
    public ICareersRepository CareersRepository { get; }
}