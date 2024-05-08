using System.Text;
using BuildingBlocks.Entity.Interfaces;
using BuildingBlocks.Entity.Repositories;
using BuildingBlocks.Errors.Models;
using BuildingBlocks.Presentation.Results;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Users.Application.Auth.Interfaces;
using Users.Application.Auth.Services;
using Users.Application.Constants;
using Users.Application.Users;
using Users.Application.Users.Interfaces;
using Users.Application.Users.Validators;
using Users.Domain.Auth;
using Users.Domain.Users;
using Users.Infrastructure.Persistence;
using Users.Infrastructure.Users.Services;

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
        services.AddApplicationConfigureServices(configuration);
    }

    private static void InfrastructureConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var uriDatabase = configuration.GetConnectionString("Database");
        var uriGrpcFileStorage = configuration.GetConnectionString("GrpcFileStorage");

        if (uriDatabase == null)
        {
            throw new ArgumentException("ConnectionString Database cannot be empty");
        }

        if (uriGrpcFileStorage == null)
        {
            throw new ArgumentException("ConnectionString GrpcFileStorage cannot be empty");
        }
        
        
        services.AddDbContext<UsersDbContext>(opt =>
        {
            opt.UseNpgsql(uriDatabase);
        });

        services.AddGrpcClient<FileStorageGrpcService.FileStorageGrpcServiceClient>(opt =>
        {
            opt.Address = new Uri(uriGrpcFileStorage);
        });

        services.AddScoped<IRepository<User>, Repository<User, UsersDbContext>>();
        services.AddScoped<IRepository<UserVerify>, Repository<UserVerify, UsersDbContext>>();
        services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken, UsersDbContext>>();
    }

    private static void AddApplicationConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<ISecurityService, SecurityService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(UpdateUserModelValidator).Assembly);
        
        var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();

        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };

                opt.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        
                        var error = new ErrorModel(401, UserConstants.Unauthorized);
                        
                        context.Response.StatusCode = error.StatusCode;
                        await context.Response.WriteAsJsonAsync(ApiResult.Failure(error));
                    }
                };
            });
    }
}