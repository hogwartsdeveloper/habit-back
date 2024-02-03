using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Models;
using Habit.Core.Entities;
using Habit.Core.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Habit.Services;

public class HabitService(
    ApplicationDbContext dbContext,
    IMapper mapper) : IHabitService
{
    public async Task<Guid> AddAsync(Guid userId, AddHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Core.Entities.Habit>(model);
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

    public async Task UpdateAsync(Guid id, UpdateHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.HabitRecords
            .Where(e => e.HabitId == id)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Habit not found");
        }

        mapper.Map(model, entity);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRecord(Guid habitId, List<HabitRecordViewModel> models, CancellationToken cancellationToken = default)
    {
        var habit = await dbContext.Habits.Where(h => h.Id == habitId).FirstOrDefaultAsync(cancellationToken);

        if (habit is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Habit not found");
        }

        models = models.OrderBy(m => m.Date).ToList();
        var first = models.FirstOrDefault();
        var last = models.LastOrDefault();

        if (habit.StartDate > first?.Date || last?.Date > habit.EndDate)
        {
            throw new HttpException(HttpStatusCode.BadRequest,
                "The date of the entries goes beyond the habit action interval");
        }

        if (last?.Date > DateTime.UtcNow)
        {
            throw new HttpException(HttpStatusCode.BadRequest,
                "The date of the entries cannot be greater than today");
        }

        var habitRecords = await dbContext.HabitRecords.Where(h => h.HabitId == habitId).ToListAsync(cancellationToken);

        if (habitRecords.LastOrDefault()?.Date > first?.Date)
        {
            throw new HttpException(HttpStatusCode.Conflict, "You are trying to edit an existing entry");
        }

        var isOverdue = false;
        var entities = mapper.Map<List<HabitRecord>>(models);
        entities.ForEach(e =>
        {
            e.HabitId = habitId;

            if (!e.IsComplete)
            {
                isOverdue = true;
            }
        });

        await dbContext.HabitRecords.AddRangeAsync(entities, cancellationToken);
        habit.IsOverdue = isOverdue;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}