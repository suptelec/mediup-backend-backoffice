using MediUp.Domain.Interfaces;
using MediUp.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Infrastructure.Persistence;
public abstract class RepositoryBase<TEntity, TDbContext> : IRepositoryBase<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    protected readonly TDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(TDbContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null) => predicate != null ? await _dbSet.CountAsync(predicate) : await _dbSet.CountAsync();

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => predicate != null ? await _dbSet.AnyAsync(predicate) : await _dbSet.AnyAsync();

    public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);

    public virtual void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            _dbSet.Update(entity);
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {

        return await _dbSet.ToListAsync();
    }

    public virtual Task<bool> ExistsById(long id)
    {
        Check.NotEmpty(id, nameof(id));

        Type entityType = typeof(TEntity);

        return ((IQueryable<IBaseEntity>)_dbSet).AnyAsync(m => m.Id == id);
    }
}
