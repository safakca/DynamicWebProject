using System.Linq.Expressions;
using EntityLayer.Common;

namespace BusinessLayer.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task<List<TEntity>> GetAllAsync();
	Task<TEntity?> GetAsync(object id);
	Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter);
	Task CreateAsync(TEntity entity);
	Task UpdateAsync(TEntity entity);
	Task RemoveAsync(TEntity entity);
}

