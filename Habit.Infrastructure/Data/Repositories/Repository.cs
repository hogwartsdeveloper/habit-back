using Habit.Core.Entities.Abstraction;
using Habit.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : EntityBase
{
    public IQueryable<T> GetByIdAsync(Guid id)
    {
        return dbContext.Set<T>()
            .Where(e => e.Id == id)
            .AsNoTracking()
            .AsQueryable();

    }

    public IQueryable<T> GetListAsync()
    {
        return dbContext.Set<T>()
            .AsNoTracking()
            .AsQueryable();
    }

    public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var addedEntity = await dbContext.Set<T>()
            .AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity.Id;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<T>()
            .Entry(entity)
            .State = EntityState.Modified;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}