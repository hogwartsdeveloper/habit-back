using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Persistence;

namespace Users.Endpoints.Extensions;

/// <summary>
/// Расширение для настройки зависимостей приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Добавляет сервисы модуля пользователей в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов для добавления новых сервисов.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения настроек.</param>
    public static void AddHabitsModuleServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddUserDbContext(configuration.GetConnectionString("UsersDb")!);
    }

    private static void AddUserDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<UsersDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });
    }
}