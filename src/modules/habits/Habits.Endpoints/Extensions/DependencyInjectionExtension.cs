using BuildingBlocks.Entity.Interfaces;
using BuildingBlocks.Entity.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Habits.Application;
using Habits.Application.Interfaces;
using Habits.Application.Services;
using Habits.Application.Validators;
using Habits.Domain.Habits;
using Habits.Infrastructure.BackgroundJobs;
using Habits.Infrastructure.BackgroundJobs.Interfaces;
using Habits.Infrastructure.Persistence;
using Hangfire;
using Hangfire.PostgreSql;
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
        services.InfrastructureConfigureServices(configuration);
        services.AddApplicationConfigureServices();
    }

    private static void InfrastructureConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HabitsDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("Database"));
        });
        
        services.AddHangfire(opt =>
        {
            opt.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            opt.UseSimpleAssemblyNameTypeSerializer();
            opt.UseRecommendedSerializerSettings();
            opt.UseColouredConsoleLogProvider();
            opt.UsePostgreSqlStorage(s => s.UseNpgsqlConnection(configuration.GetConnectionString("Database")));
        });

        services.AddHangfireServer();

        services.AddScoped<IRepository<Domain.Habits.Habit>, Repository<Domain.Habits.Habit, HabitsDbContext>>();
        services.AddScoped<IRepository<HabitRecord>, Repository<HabitRecord, HabitsDbContext>>();

        services.AddScoped<IHabitJob, HabitJob>();
    }

    private static void AddApplicationConfigureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(HabitMapperProfile).Assembly);
        services.AddScoped<IHabitService, HabitService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(AddHabitModelValidator).Assembly);
    }
}