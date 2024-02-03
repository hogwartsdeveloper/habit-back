using Habit.Application.BackgroundJobs.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.BackgroundJobs;

public class HabitJob(ApplicationDbContext dbContext) : IHabitJob
{
    public async Task CheckIsOverdueAsync()
    {
        var habits = await dbContext.Habits
            .Where(e => !e.IsOverdue)
            .Include(habit => habit.HabitRecords)
            .ToListAsync();

        foreach (var habit in habits)
        {
            var last = habit.HabitRecords?.LastOrDefault();

            if (last is null || last?.Date < DateTime.UtcNow)
            {
                habit.IsOverdue = true;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}