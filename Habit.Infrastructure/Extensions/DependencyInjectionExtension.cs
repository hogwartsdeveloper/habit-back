using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Data;
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
            opt.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(connectionString));
        });

        service.AddHangfireServer();
    }
}