using AutoMapper;
using AutoMapper.QueryableExtensions;
using HabitServer.Entities;
using HabitServer.Models.Habits;
using HabitServer.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HabitServer.Services;

public class HabitService(
    ApplicationDbContext dbContext,
    IMapper mapper) : IHabitService
{
    public async Task<Guid> AddAsync(AddHabitModel viewModel, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Habit>(mapper.ConfigurationProvider);

        var addedEntity = await dbContext.Habits.AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity.Id;
    }

    public async Task<List<HabitViewModel>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Habits
            .ProjectTo<HabitViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public Task<HabitViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Habits
            .Where(entity => entity.Id == id)
            .ProjectTo<HabitViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateHabitModel viewModel, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Habit>(mapper.ConfigurationProvider);
        dbContext.Habits.Update(entity);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}