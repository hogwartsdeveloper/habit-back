using Habit.Core.Entities.Abstraction;

namespace Habit.Core.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    IQueryable<T> GetByIdAsync(Guid id);
    IQueryable<T> GetListAsync();
    Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
}