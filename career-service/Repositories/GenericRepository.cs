using System.Linq.Expressions;
using career_service.Data;
using career_service.Models;
using career_service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace career_service.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected DataContext context;
    protected DbSet<TEntity> dbSet;
    
    public GenericRepository(DataContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }
    
    public virtual async Task<List<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;
        

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }
        return await query.ToListAsync();
    }
    
    
    
}