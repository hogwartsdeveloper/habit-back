namespace Habit.Core.Interfaces;

public interface IRepository<T>
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetListAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}