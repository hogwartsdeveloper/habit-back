using Habit.Application.Repositories;
using Infrastructure.BackgroundJobs.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BackgroundJobs;

public class HabitJob(IRepository<Habit.Domain.Entities.Habit> habitRepository) : IHabitJob
{
    public async Task CheckIsOverdueAsync()
    {
        
        var habits = await habitRepository
            .GetListAsync(e => !e.IsOverdue)
            .Include(h => h.HabitRecords)
            .ToListAsync();

        var updateHabits = new List<Habit.Domain.Entities.Habit>();

        foreach (var habit in habits)
        {
            var last = habit.HabitRecords?.LastOrDefault();

            if (last is null || last?.Date < DateTime.UtcNow)
            {
                habit.IsOverdue = true;
                updateHabits.Add(habit);
            }
        }

        await habitRepository.UpdateRangeAsync(updateHabits);
    }
}