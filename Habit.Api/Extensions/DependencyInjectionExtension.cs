using Habit.Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

namespace Habit.Api.Extensions;

public static class DependencyInjectionExtension
{
    public static void InfrastructureConfigureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddApplicationDbContext(configuration.GetConnectionString("Database"));
        service.AddTaskManager(configuration.GetConnectionString("HangfireDb"));
        service.AddRepositories();
    }

    public static void ApplicationConfigureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.ApplicationAuthenticationConfigure(configuration);
        service.AddApplicationServices(configuration);
        service.AddControllers();
        service.AddEndpointsApiExplorer();
        
        service.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "HabitAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
    
}