using Habits.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Habits.Endpoints.Extensions;

/// <summary>
/// Расширение для настройки зависимостей приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Добавляет сервисы модуля привычек в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов для добавления новых сервисов.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения настроек.</param>
    public static void AddHabitsModuleServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHabitDbContext(configuration.GetConnectionString("HabitsDb")!);
    }

    private static void AddHabitDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<HabitsDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });
    }
}