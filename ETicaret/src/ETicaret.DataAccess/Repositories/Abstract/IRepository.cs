using System.Linq.Expressions;
using ETicaret.Entities.Common;

namespace ETicaret.DataAccess.Repositories.Abstract;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes);
    IQueryable<T> Query();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    void SoftDelete(T entity);
}
