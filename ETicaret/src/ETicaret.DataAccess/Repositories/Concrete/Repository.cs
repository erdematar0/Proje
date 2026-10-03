using System.Linq.Expressions;
using ETicaret.DataAccess.Context;
using ETicaret.DataAccess.Repositories.Abstract;
using ETicaret.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.DataAccess.Repositories.Concrete;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> Table;

    public Repository(AppDbContext context)
    {
        Context = context;
        Table = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id) => await Table.FindAsync(id);

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Table;
        foreach (var include in includes) query = query.Include(include);
        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Table.AsNoTracking();
        foreach (var include in includes) query = query.Include(include);
        if (predicate is not null) query = query.Where(predicate);
        return await query.ToListAsync();
    }

    public IQueryable<T> Query() => Table.AsQueryable();

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => Table.AnyAsync(predicate);

    public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        => predicate is null ? Table.CountAsync() : Table.CountAsync(predicate);

    public async Task AddAsync(T entity) => await Table.AddAsync(entity);

    public void Update(T entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        Table.Update(entity);
    }

    public void Remove(T entity) => Table.Remove(entity);

    public void SoftDelete(T entity)
    {
        entity.IsActive = false;
        Update(entity);
    }
}
