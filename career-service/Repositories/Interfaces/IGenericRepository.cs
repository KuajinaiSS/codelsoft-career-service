using System.Linq.Expressions;

namespace career_service.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retrieves a list of entities based on the provided filter, order, and inclusion criteria.
    /// </summary>
    /// <param name="filter">A predicate to filter entities, or null for no filtering.</param>
    /// <param name="orderBy">A function to specify the order of entities, or null for no ordering.</param>
    /// <param name="includeProperties">A comma-separated list of navigation properties to include, or an empty string for no inclusion.</param>
    /// <returns>A list of entities that meet the criteria.</returns>
    Task<List<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
    
}