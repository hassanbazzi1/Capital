using System.Linq.Expressions;

namespace PhoenicianCapital.Repositiory.Interface;

public interface IDbRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

    Task<int> SaveChangesAsync();
}
