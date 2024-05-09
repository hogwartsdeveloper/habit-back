using BuildingBlocks.Validation.Interceptors;
using FileStorage.Endpoints.Extensions;
using FluentValidation.AspNetCore;
using Habits.Endpoints.Extensions;
using MassTransit;
using Microsoft.OpenApi.Models;
using Notifications.Infrastructure.Extensions;
using Users.Endpoints.Extensions;

namespace App.Extensions;

/// <summary>
/// Расширение для настройки зависимостей в инфраструктуре приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Настраивает сервисы инфраструктуры.
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void InfrastructureConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHabitsModuleServices(configuration);
        services.AddUsersModuleServices(configuration);
        services.AddFileStorageModule(configuration);
        services.AddNotificationModule(configuration);

        services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();

        services.AddMassTransit(opt =>
        {
            opt.AddFileStorageConsumers();
            opt.AddNotificationConsumers();
            opt.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitConfiguration = configuration.GetSection("RabbitMQ");
                cfg.Host(
                    rabbitConfiguration.GetSection("HostName").Value,
                    rabbitConfiguration.GetSection("VirtualHost").Value, h =>
                    {
                        h.Username(rabbitConfiguration.GetSection("UserName").Value ?? "guest");
                        h.Password(rabbitConfiguration.GetSection("Password").Value ?? "guest");
                    });
                
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }

    /// <summary>
    /// Настраивает сервисы приложения.
    /// </summary>
    /// <param name="service">Коллекция сервисов для регистрации зависимостей.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void ApplicationConfigureServices(this IServiceCollection service, IConfiguration configuration)
    {
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