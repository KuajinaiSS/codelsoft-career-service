using career_service.Data;
using career_service.Models;

namespace career_service.Repositories.Interfaces;

public class SubjectsRepository : GenericRepository<Subject>, ISubjectsRepository
{
    public SubjectsRepository(DataContext context) : base(context)
    {
    }
    
}