using BusinessLayer.Repositories;
using DataAccessLayer.Concrete;
using EntityLayer.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly Context _context;

    public BaseRepository(Context context) => _context = context;

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetAsync(object id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(filter);
    }

    public async Task<bool> RemoveAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}

