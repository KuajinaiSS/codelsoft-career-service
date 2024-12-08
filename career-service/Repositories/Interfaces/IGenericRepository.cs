using System.Linq.Expressions;

namespace career_service.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{

    Task<List<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
    

    Task<TEntity?> GetByiD(object id);
    
}