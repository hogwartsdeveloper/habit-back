using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Mail.Interfaces;
using Notifications.Application.Mail.Models;
using Notifications.Application.Mail.Services;

namespace Notifications.Application.Extensions;

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
    }
}