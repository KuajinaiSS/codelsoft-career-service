using career_service.Data;
using career_service.Models;
using career_service.Repositories.Interfaces;

namespace career_service.Repositories;

public class CareersRepository : GenericRepository<Career>, ICareersRepository
{
    public CareersRepository(DataContext context) : base(context)
    {
    }
    
}