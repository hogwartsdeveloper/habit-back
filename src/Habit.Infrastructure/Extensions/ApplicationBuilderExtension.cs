using Hangfire;
using Infrastructure.BackgroundJobs.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

/// <summary>
/// Расширение настройки приложения.
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Добавляет фоновые задачи в приложение.
    /// </summary>
    /// <param name="_">Построитель приложения.</param>
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
            .Enqueue<IBrokerMessageListenerJob>(listener => listener.UserVerifyStartListening());
    }
}