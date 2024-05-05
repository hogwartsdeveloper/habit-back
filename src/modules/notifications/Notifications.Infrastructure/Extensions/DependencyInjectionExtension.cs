using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Mail.Interfaces;
using Notifications.Application.Mail.Models;
using Notifications.Application.Mail.Services;
using Notifications.Infrastructure.Consumers;

namespace Notifications.Infrastructure.Extensions;

/// <summary>
/// Расширение для настройки зависимостей приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Добавляет сервисы модуля Notification в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов для добавления новых сервисов.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения настроек.</param>
    public static void AddNotificationModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MailSettingsModel>(configuration.GetSection("MailSettings"));
        services.AddSingleton<IMailService, MailService>();
        
        services.InfrastructureConfigureServices(configuration.GetSection("RabbitMQ"));
    }
    
    private static void InfrastructureConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(opt =>
        {
            opt.AddConsumer<SendMessageConsumer>();
            
            opt.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(
                    configuration.GetSection("HostName").Value,
                    configuration.GetSection("VirtualHost").Value, h =>
                    {
                        h.Username(configuration.GetSection("UserName").Value ?? "guest");
                        h.Password(configuration.GetSection("Password").Value ?? "guest");
                    });
                
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}