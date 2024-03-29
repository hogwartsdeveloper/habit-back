using Habit.Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

namespace Habit.Api.Extensions;

/// <summary>
/// Расширение для настройки зависимостей в инфраструктуре приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Настраивает сервисы инфраструктуры.
    /// </summary>
    /// <param name="service">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void InfrastructureConfigureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddApplicationDbContext(configuration.GetConnectionString("Database"));
        service.AddTaskManager(configuration.GetConnectionString("Database"));
        service.AddRepositories();
        service.AddBrokerMessageService(configuration);
        service.AddFileStorageServices(configuration);
        service.AddBackgroundJobs();
        service.AddInterceptors();
    }

    /// <summary>
    /// Настраивает сервисы приложения.
    /// </summary>
    /// <param name="service">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void ApplicationConfigureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.ApplicationAuthenticationConfigure(configuration);
        service.AddApplicationServices(configuration);
        service.AddApplicationValidations();
        service.AddCors();
        service.AddControllers()
            .ConfigureApiBehaviorOptions(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });
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