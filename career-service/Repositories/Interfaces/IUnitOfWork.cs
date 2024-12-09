namespace career_service.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the careers repository.
    /// </summary>
    /// <value>A Concrete class for ICareersRepository</value>
    public ICareersRepository CareersRepository { get; }
    
    /// <summary>
    /// Gets the subjects repository.
    /// </summary>
    /// <value>A Concrete class for ISubjectsRepository</value>
    public ISubjectsRepository SubjectsRepository { get; }

    /// <summary>
    /// Gets the subject relationships' repository.
    /// </summary>
    /// <value>A Concrete class for ISubjectRelationshipsRepository</value>
    public ISubjectRelationshipsRepository SubjectRelationshipsRepository { get; }
}