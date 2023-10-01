using DataLayer.DbContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public abstract class GenericRepository<T> : IBaseRepository<T> where T: BaseEntity
{
    private LibraryContext db;

    protected GenericRepository(LibraryContext db)
    {
        this.db = db;
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await db.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return db.Set<T>().AsNoTracking().AsEnumerable();
    }

    public async Task<T> AddAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await db.Set<T>().AsNoTracking().AnyAsync(p => p.Id == id);
    }
}