using Hangfire;
using Infrastructure.BackgroundJobs.Interfaces;
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

        BackgroundJob
            .Enqueue<IBrokerMessageListenerJob>(listener => listener.StartListening());
    }
}