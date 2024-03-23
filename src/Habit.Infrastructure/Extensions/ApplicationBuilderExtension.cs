using Hangfire;
using Infrastructure.BackgroundJobs.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    
    public static async Task MigrationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}