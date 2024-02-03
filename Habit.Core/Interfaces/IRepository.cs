using System.Linq.Expressions;
using Habit.Core.Entities.Abstraction;

namespace Habit.Core.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    IQueryable<T> GetByIdAsync(Guid id);
    IQueryable<T> GetListAsync();
    IQueryable<T> GetListAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}