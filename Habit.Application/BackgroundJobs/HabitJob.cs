using Habit.Application.BackgroundJobs.Interfaces;
using Habit.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.BackgroundJobs;

public class HabitJob(IRepository<Core.Entities.Habit> habitRepository) : IHabitJob
{
    public async Task CheckIsOverdueAsync()
    {
        
        var habits = await habitRepository
            .GetListAsync(e => !e.IsOverdue)
            .Include(h => h.HabitRecords)
            .ToListAsync();

        var updateHabits = new List<Core.Entities.Habit>();

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