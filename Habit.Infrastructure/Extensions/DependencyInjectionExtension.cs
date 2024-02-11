using Habit.Application.BrokerMessage;
using Habit.Application.Repositories;
using Habit.Domain.Entities;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.BackgroundJobs;
using Infrastructure.BackgroundJobs.Interfaces;
using Infrastructure.BrokerMessage;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddApplicationDbContext(this IServiceCollection service, string? connectionString)
    {
        service.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });
    }

    public static void AddTaskManager(this IServiceCollection service, string? connectionString)
    {
        service.AddHangfire(opt =>
        {
            opt.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            opt.UseSimpleAssemblyNameTypeSerializer();
            opt.UseRecommendedSerializerSettings();
            opt.UseColouredConsoleLogProvider();
            opt.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(connectionString));
        });

        service.AddHangfireServer();
    }

    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IRepository<Habit.Domain.Entities.Habit>, Repository<Habit.Domain.Entities.Habit>>();
        service.AddScoped<IRepository<HabitRecord>, Repository<HabitRecord>>();
        service.AddScoped<IRepository<User>, Repository<User>>();
        service.AddScoped<IRepository<UserVerify>, Repository<UserVerify>>();
        service.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
    }

    public static void AddBrokerMessageService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BrokerMessageSettings>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton<IBrokerMessageService, BrokerMessageService>();
    }

    public static void AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddScoped<IHabitJob, HabitJob>();
        services.AddScoped<IBrokerMessageListenerJob, BrokerMessageListenerJob>();
    }
}