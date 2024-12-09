using career_service.Data;
using career_service.Repositories.Interfaces;

namespace career_service.Repositories;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private ICareersRepository _careersRepository = null!;
    private ISubjectsRepository _subjectsRepository = null!;
    private ISubjectRelationshipsRepository _subjectRelationshipsRepository = null!;

    private bool _disposed = false;
    
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    
    private readonly DataContext _context;
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing) _context.Dispose();
        }
        _disposed = true;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    public ICareersRepository CareersRepository
    {
        get
        {
            _careersRepository ??= new CareersRepository(_context);
            return _careersRepository;
        }
    }
    
    public ISubjectsRepository SubjectsRepository
    {
        get
        {
            _subjectsRepository ??= new SubjectsRepository(_context);
            return _subjectsRepository;
        }
    }

    public ISubjectRelationshipsRepository SubjectRelationshipsRepository
    {
        get
        {
            _subjectRelationshipsRepository ??= new SubjectRelationshipsRepository(_context);
            return _subjectRelationshipsRepository;
        }
    }
}
