using FileStorage.Application.FileStorage.Interfaces;
using FileStorage.Infrastructure.Consumers;
using FileStorage.Infrastructure.Services;
using FileStorage.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileStorage.Endpoints.Extensions;

/// <summary>
/// Расширение для настройки зависимостей приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Добавляет сервисы модуля fileStorage в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов для добавления новых сервисов.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения настроек.</param>
    public static void AddFileStorageModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpc();
        services.Configure<FileStorageSettings>(configuration.GetSection("MinIO"));
        services.AddSingleton<IFileStorageService, FileStorageService>();
    }

    public static void AddFileStorageConsumers(this IBusRegistrationConfigurator opt)
    {
        opt.AddConsumer<RemoveFileConsumer>();
    }
}