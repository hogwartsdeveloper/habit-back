using System.Linq.Expressions;
using Habit.Application.Repositories;
using Habit.Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Репозиторий для доступа к данным сущности.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : EntityBase
{
    /// <inheritdoc />
    public IQueryable<T> GetByIdAsync(Guid id)
    {
        return dbContext
            .Set<T>()
            .Where(e => e.Id == id)
            .AsNoTracking()
            .AsQueryable();
    }

    /// <inheritdoc />
    public IQueryable<T> GetListAsync()
    {
        return dbContext
            .Set<T>()
            .AsNoTracking()
            .AsQueryable();
    }

    /// <inheritdoc />
    public IQueryable<T> GetListAsync(Expression<Func<T, bool>> predicate)
    {
        return dbContext
            .Set<T>()
            .Where(predicate)
            .AsNoTracking()
            .AsQueryable();
    }

    /// <inheritdoc />
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return dbContext
            .Set<T>()
            .AnyAsync(predicate, cancellationToken);
    }
    
    /// <inheritdoc />
    public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var addedEntity = await dbContext
            .Set<T>()
            .AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity.Id;
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await dbContext
            .Set<T>()
            .AddRangeAsync(entities, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext
            .Set<T>()
            .Update(entity);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        dbContext
            .Set<T>()
            .UpdateRange(entities);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}