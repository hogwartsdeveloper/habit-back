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

    public async Task AddRecords(Guid id, List<AddHabitCalendarModel> models, CancellationToken cancellationToken = default)
    {
        var habit = await dbContext.Habits.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (habit is null)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Habit not found");
        }

        if (habit.IsOverdue)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Habit is overdue");
        }

        models = models.OrderBy(m => m.Date).ToList();
        var firstDate = models.FirstOrDefault();
        var lastDate = models.LastOrDefault();

        if (firstDate?.Date < habit.StartDate || lastDate?.Date > habit.EndDate)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "The recording date is not between StartDate and EndDate");
        }
        
        if (lastDate?.Date > DateTime.UtcNow)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "Recording date is greater than today");
        }

        var habitCalendarLastDate = habit.Calendar?.LastOrDefault();

        if (habitCalendarLastDate?.Date > firstDate?.Date)
        {
            throw new HttpException(HttpStatusCode.BadRequest, "There is already a record on the selected dates");
        }

        var habitCalendars = mapper.Map<List<HabitCalendar>>(models);

        if (habit.Calendar == null)
        {
            habit.Calendar = habitCalendars.ToArray();
        }
        else
        {
            habit.Calendar.ToList().AddRange(habitCalendars);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
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