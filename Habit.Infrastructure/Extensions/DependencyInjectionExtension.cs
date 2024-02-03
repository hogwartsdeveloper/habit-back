using Habit.Core.Interfaces;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
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
        service.AddScoped<IRepository<Habit.Core.Entities.Habit>, Repository<Habit.Core.Entities.Habit>>();
    }
}