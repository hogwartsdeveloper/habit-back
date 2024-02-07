using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Models;
using Habit.Application.Repositories;
using Habit.Core.Entities;
using Habit.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Habit.Services;

public class HabitService(
    IRepository<Core.Entities.Habit> habitRepository,
    IRepository<HabitRecord> habitRecordRepository,
    IMapper mapper) : IHabitService
{
    public Task<Guid> AddAsync(Guid userId, AddHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<Core.Entities.Habit>(model);
        entity.UserId = userId;

        return habitRepository.AddAsync(entity, cancellationToken);
    }

    public async Task<List<HabitViewModel>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await habitRepository
            .GetListAsync()
            .ProjectTo<HabitViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public Task<HabitViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return habitRepository
            .GetByIdAsync(id)
            .ProjectTo<HabitViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateHabitModel model, CancellationToken cancellationToken = default)
    {
        var entity = await GetAndValidateHabitAsync(id, cancellationToken);
        
        mapper.Map(model, entity);
        await habitRepository.UpdateAsync(entity, cancellationToken);
    }

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
            e.HabitId = habitId;

            if (!e.IsComplete)
            {
                isOverdue = true;
            }
        });
        await habitRecordRepository.AddRangeAsync(entities, cancellationToken);
        habit.IsOverdue = isOverdue;

        await habitRepository.UpdateAsync(habit, cancellationToken);
    }

    private async Task<Core.Entities.Habit> GetAndValidateHabitAsync(Guid id, CancellationToken cancellationToken)
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