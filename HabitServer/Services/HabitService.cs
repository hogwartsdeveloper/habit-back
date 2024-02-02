using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HabitServer.Entities;
using HabitServer.Exception;
using HabitServer.Models;
using HabitServer.Models.Habits;
using HabitServer.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HabitServer.Services;

public class HabitService(
    ApplicationDbContext dbContext,
    IMapper mapper) : IHabitService
{
    public async Task<Guid> AddAsync(Guid userId, AddHabitModel viewModel, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Habit>(viewModel);
        entity.UserId = userId;

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

    public async Task UpdateAsync(Guid userId, Guid id, UpdateHabitModel viewModel, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Habit>(viewModel);
        entity.UserId = userId;
        dbContext.Habits.Update(entity);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}