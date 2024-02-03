using Habit.Application.BackgroundJobs.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

public static class ApplicationBuilderExtension
{
    public static void AddBackgroundJobs(this IApplicationBuilder _)
    {
        RecurringJob.AddOrUpdate<IHabitJob>(
            "CheckHabit",
            job => job.CheckIsOverdueAsync(),
            Cron.Daily(23, 50),
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });
    }
}