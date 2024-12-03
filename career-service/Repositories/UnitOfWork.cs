using career_service.Data;
using career_service.Repositories.Interfaces;

namespace career_service.Repositories;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private ICareersRepository _careersRepository = null!;

    private bool _disposed = false;
    
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    
    private readonly DataContext _context;
    
    public ICareersRepository CareersRepository
    {
        get
        {
            _careersRepository ??= new CareersRepository(_context);
            return _careersRepository;
        }
    }
    
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
}