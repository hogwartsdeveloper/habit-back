using BuildingBlocks.Entity.Interfaces;
using BuildingBlocks.Entity.Repositories;
using BuildingBlocks.IntegrationEvents.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Auth.Interfaces;
using Users.Application.Auth.Services;
using Users.Application.Users;
using Users.Application.Users.Interfaces;
using Users.Application.Users.Services;
using Users.Application.Users.Validators;
using Users.Domain.Auth;
using Users.Domain.Users;
using Users.Infrastructure.Persistence;

namespace Users.Endpoints.Extensions;

/// <summary>
/// Расширение для настройки зависимостей приложения.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Добавляет сервисы модуля пользователей в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов для добавления новых сервисов.</param>
    /// <param name="configuration">Конфигурация приложения, используемая для получения настроек.</param>
    public static void AddUsersModuleServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.InfrastructureConfigureServices(configuration);
        services.AddApplicationConfigureServices();
    }

    private static void InfrastructureConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("UsersDb")!);
        });

        services.AddScoped<IRepository<User>, Repository<User, UsersDbContext>>();
        services.AddScoped<IRepository<UserVerify>, Repository<UserVerify, UsersDbContext>>();
        services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken, UsersDbContext>>();
        services.AddIntegrationEventBus(configuration.GetSection("RabbitMQ"));
    }

    private static void AddApplicationConfigureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<ISecurityService, SecurityService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(UpdateUserModelValidator).Assembly);
    }
}