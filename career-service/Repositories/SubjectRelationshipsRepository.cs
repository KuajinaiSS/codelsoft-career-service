using career_service.Data;
using career_service.Models;
using career_service.Repositories.Interfaces;

namespace career_service.Repositories;

public class SubjectRelationshipsRepository : GenericRepository<SubjectRelationship>, ISubjectRelationshipsRepository
{
    public SubjectRelationshipsRepository(DataContext context) : base(context)
    {
    }
    
}