using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MediUp.Domain.Interfaces;
public interface IRepositoryBase<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entity);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> GetAllAsync();
    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entity);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    Task<bool> ExistsById(long id);

}
