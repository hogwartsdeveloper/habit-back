using Habits.Infrastructure.BackgroundJobs.Interfaces;
using Habits.Infrastructure.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Habits.Endpoints.Extensions;

/// <summary>
/// Расширение настройки приложения.
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Инициализация модуля привычек в приложении.
    /// </summary>
    /// <param name="app">Экземпляр приложения, к которому применяется расширение.</param>
    public static async Task HabitModuleInit(this WebApplication app)
    {
        await app.MigrationAsync();
        AddBackgroundJobs();
    }

    private static void AddBackgroundJobs()
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

    private static async Task MigrationAsync(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<HabitsDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}