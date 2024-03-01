using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Models;
using Habit.Application.Repositories;
using Habit.Domain.Entities;
using Habit.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Habit.Services;

/// <inheritdoc />
public class HabitService(
    IRepository<Domain.Entities.Habit> habitRepository,
    IRepository<HabitRecord> habitRecordRepository,
    IMapper mapper) : IHabitService
{
    /// <inheritdoc />
    public Task<Guid> AddAsync(Guid userId, AddHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Domain.Entities.Habit>(model);
        entity.SetUser(userId);

        return habitRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<HabitViewModel>> GetListAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await habitRepository
            .GetListAsync()
            .Where(h => h.UserId == userId)
            .ProjectTo<HabitViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<HabitGroupViewsModel> GetListGroupAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var habits = await GetListAsync(userId, cancellationToken);
        
        var active = new List<HabitViewModel>();
        var overdue = new List<HabitViewModel>();

        foreach (var habit in habits)
        {
            if (habit.IsOverdue)
            {
                overdue.Add(habit);
                continue;
            }
            
            active.Add(habit);
        }

        return new HabitGroupViewsModel
        {
            Active = active,
            Overdue = overdue
        };
    }

    /// <inheritdoc />
    public Task<HabitViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return habitRepository
            .GetByIdAsync(id)
            .ProjectTo<HabitViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, UpdateHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = await GetAndValidateHabitAsync(id, cancellationToken);
        
        mapper.Map(model, entity);
        await habitRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddRecord(Guid habitId, List<HabitRecordViewModel> models, CancellationToken cancellationToken = default)
    {
        var habit = await GetAndValidateHabitAsync(habitId, cancellationToken);

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

        
        var habitRecords = await habitRecordRepository
            .GetListAsync(h => h.HabitId == habitId)
            .ToListAsync(cancellationToken);

        if (habitRecords.LastOrDefault()?.Date > first?.Date)
        {
            throw new HttpException(HttpStatusCode.Conflict, "You are trying to edit an existing entry");
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

        await habitRepository.UpdateAsync(habit, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid habitId, CancellationToken cancellationToken)
    {
        await habitRepository.DeleteAsync(habitId, cancellationToken);
    }

    private async Task<Domain.Entities.Habit> GetAndValidateHabitAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await habitRepository
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Habit not found");
        }

        return entity;
    }
}