using HabitServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitServer.Extensions;

public static class DependencyInjectionExtension
{
    public static void InfrastructureConfigureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("Database"));
        });
    }
}