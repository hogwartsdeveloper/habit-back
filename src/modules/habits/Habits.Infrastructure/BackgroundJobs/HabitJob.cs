using BuildingBlocks.Entity.Interfaces;
using Habits.Infrastructure.BackgroundJobs.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Habits.Infrastructure.BackgroundJobs;

/// <inheritdoc />
public class HabitJob(IRepository<Domain.Habits.Habit> habitRepo)
    : IHabitJob
{
    /// <inheritdoc />
    public async Task CheckIsOverdueAsync()
    {
        var habits = await habitRepo
            .GetListAsync(e => !e.IsOverdue)
            .Include(h => h.HabitRecords)
            .ToListAsync();

        var updateHabits = new List<Domain.Habits.Habit>();

        foreach (var habit in habits)
        {
            var last = habit.HabitRecords?.LastOrDefault();

            if (last is null || last?.Date < DateTime.UtcNow)
            {
                habit.SetOverdue(true);
                updateHabits.Add(habit);
            }
        }

        await habitRepo.UpdateRangeAsync(updateHabits);
    }
}