using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingBlocks.Entity.Interfaces;
using BuildingBlocks.Errors.Exceptions;
using Habits.Application.Constants;
using Habits.Application.Interfaces;
using Habits.Application.Models;
using Habits.Domain.Habits;
using Microsoft.EntityFrameworkCore;

namespace Habits.Application.Services;

/// <inheritdoc />
public class HabitService(
    IRepository<Domain.Habits.Habit> habitRepo,
    IRepository<HabitRecord> habitRecordRepository,
    IMapper mapper) : IHabitService
{
    /// <inheritdoc />
    public Task<Guid> AddAsync(Guid userId, AddHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Domain.Habits.Habit>(model);
        entity.SetUser(userId);

        return habitRepo.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<ViewHabitModel>> GetListAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await habitRepo
            .GetListAsync()
            .Where(h => h.UserId == userId)
            .ProjectTo<ViewHabitModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ViewHabitGroupModel> GetListGroupAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var habits = await GetListAsync(userId, cancellationToken);
        
        var active = new List<ViewHabitModel>();
        var overdue = new List<ViewHabitModel>();

        foreach (var habit in habits)
        {
            if (habit.IsOverdue)
            {
                overdue.Add(habit);
                continue;
            }
            
            active.Add(habit);
        }

        return new ViewHabitGroupModel
        {
            Active = active,
            Overdue = overdue
        };
    }

    /// <inheritdoc />
    public Task<ViewHabitModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return habitRepo
            .GetByIdAsync(id)
            .ProjectTo<ViewHabitModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, UpdateHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = await GetAndValidateHabitAsync(id, cancellationToken);
        
        mapper.Map(model, entity);
        await habitRepo.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddRecord(Guid habitId, List<ViewHabitRecordModel> models, CancellationToken cancellationToken = default)
    {
        var habit = await GetAndValidateHabitAsync(habitId, cancellationToken);

        models = models.OrderBy(m => m.Date).ToList();
        var first = models.FirstOrDefault();
        var last = models.LastOrDefault();

        if (habit.StartDate > first?.Date || last?.Date > habit.EndDate)
        {
            throw new HttpException(HttpStatusCode.BadRequest,
                HabitConstant.RecordDateInterval);
        }

        if (last?.Date > DateTime.UtcNow)
        {
            throw new HttpException(HttpStatusCode.BadRequest,
                HabitConstant.RecordDayGreaterToday);
        }

        
        var habitRecords = await habitRecordRepository
            .GetListAsync(h => h.HabitId == habitId)
            .ToListAsync(cancellationToken);

        if (habitRecords.LastOrDefault()?.Date > first?.Date)
        {
            throw new HttpException(HttpStatusCode.Conflict, HabitConstant.RecordEditExistEntry);
        }

        var isOverdue = false;
        var entities = mapper.Map<List<HabitRecord>>(models);
        entities.ForEach(e =>
        {
            e.SetHabit(habitId);

            if (!e.IsComplete)
            {
                isOverdue = true;
            }
        });
        await habitRecordRepository.AddRangeAsync(entities, cancellationToken);
        habit.SetOverdue(isOverdue);

        await habitRepo.UpdateAsync(habit, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid habitId, CancellationToken cancellationToken = default)
    {
        await habitRepo.DeleteAsync(habitId, cancellationToken);
    }
    
    private async Task<Domain.Habits.Habit> GetAndValidateHabitAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await habitRepo
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, HabitConstant.NotFound);
        }

        return entity;
    }
}