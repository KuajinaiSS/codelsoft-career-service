using career_service.Data;
using career_service.Repositories.Interfaces;
using Career = CareerProto.Career;

namespace career_service.Repositories;

public class CareersRepository : GenericRepository<Models.Career>, ICareersRepository
{
    public CareersRepository(DataContext context) : base(context)
    {
    }
    
}