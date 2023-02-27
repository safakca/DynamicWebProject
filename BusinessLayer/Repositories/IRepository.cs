using EntityLayer.Common;
using System.Linq.Expressions;

namespace BusinessLayer.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(object id);
    Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> RemoveAsync(TEntity entity);
}

