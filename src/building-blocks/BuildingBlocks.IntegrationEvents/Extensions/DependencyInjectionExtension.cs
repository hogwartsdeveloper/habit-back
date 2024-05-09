using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.IntegrationEvents.Extensions;

/// <summary>
/// Предоставляет методы расширения для внедрения зависимостей,
/// связанных с интеграционными событиями через MassTransit и RabbitMQ.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Регистрирует и конфигурирует сервисы для обработки интеграционных событий
    /// с использованием MassTransit и RabbitMQ на основе конфигурации приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов DI, к которой добавляется конфигурация.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для настройки подключения к RabbitMQ.</param>
    public static void AddIntegrationEventBus(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(opt =>
        {
            opt.UsingRabbitMq((_, cfg) =>
            {
                cfg.Host(
                    configuration.GetSection("HostName").Value,
                    configuration.GetSection("VirtualHost").Value, h =>
                    {
                        h.Username(configuration.GetSection("UserName").Value ?? "guest");
                        h.Password(configuration.GetSection("Password").Value ?? "guest");
                    });
            });
        });
    }
}