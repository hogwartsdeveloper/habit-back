using Habit.Application.BrokerMessage;
using Habit.Application.FileStorage.Interfaces;
using Habit.Application.Repositories;
using Habit.Domain.Entities;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.BackgroundJobs;
using Infrastructure.BackgroundJobs.Interfaces;
using Infrastructure.BrokerMessage;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.FileStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

/// <summary>
/// Расширение для настройки зависимостей приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Добавляет контекст базы данных приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    public static void AddApplicationDbContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }

    /// <summary>
    /// Добавляет менеджер задач.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <param name="connectionString">Строка подключения к базе данных.</param>
    public static void AddTaskManager(this IServiceCollection services, string? connectionString)
    {
        services.AddHangfire(opt =>
        {
            opt.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            opt.UseSimpleAssemblyNameTypeSerializer();
            opt.UseRecommendedSerializerSettings();
            opt.UseColouredConsoleLogProvider();
            opt.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(connectionString));
        });

        services.AddHangfireServer();
    }

    /// <summary>
    /// Добавляет репозитории в коллекцию сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Habit.Domain.Entities.Habit>, Repository<Habit.Domain.Entities.Habit>>();
        services.AddScoped<IRepository<HabitRecord>, Repository<HabitRecord>>();
        services.AddScoped<IRepository<User>, Repository<User>>();
        services.AddScoped<IRepository<UserVerify>, Repository<UserVerify>>();
        services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
    }

    /// <summary>
    /// Добавляет сервис для работы с сообщениями брокера.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void AddBrokerMessageService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BrokerMessageSettings>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton<IBrokerMessageService, BrokerMessageService>();
    }

    /// <summary>
    /// Добавляет фоновые задачи в коллекцию сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    public static void AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddScoped<IHabitJob, HabitJob>();
        services.AddScoped<IBrokerMessageListenerJob, BrokerMessageListenerJob>();
    }

    /// <summary>
    /// Добавляет сервисы хранилища файлов в коллекцию сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void AddFileStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileStorageSettings>(configuration.GetSection("MinIO"));
        services.AddSingleton<IFileStorageService, FileStorageService>();
    }
}