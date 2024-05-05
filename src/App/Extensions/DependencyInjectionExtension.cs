using BuildingBlocks.IntegrationEvents.Extensions;
using Habits.Endpoints.Extensions;

namespace App.Extensions;

/// <summary>
/// Расширение для настройки зависимостей в инфраструктуре приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Настраивает сервисы инфраструктуры.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void InfrastructureConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIntegrationEventBus(configuration.GetSection("RabbitMQ"));
        services.AddHabitsModuleServices(configuration);
    }
}