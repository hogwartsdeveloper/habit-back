using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Habit.Application.Auth;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Services;
using Habit.Application.Auth.Validators;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Services;
using Habit.Application.Mail.Interfaces;
using Habit.Application.Mail.Models;
using Habit.Application.Mail.Services;
using Habit.Application.Users.Interfaces;
using Habit.Application.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Habit.Application.Extensions;

/// <summary>
/// Расширение для внедрения зависимостей.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// Добавляет службы приложения.
    /// </summary>
    /// <param name="service">Коллекция служб.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAutoMapper(typeof(AuthMapperProfile).Assembly);
        service.AddSingleton<ISecurityService, SecurityService>();
        service.AddScoped<IAuthService, AuthService>();
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IHabitService, HabitService>();
        service.AddSingleton<IMailService, MailService>();
        service.Configure<MailSettings>(configuration.GetSection("MailSettings"));
    }

    /// <summary>
    /// Конфигурирует аутентификацию приложения.
    /// </summary>
    /// <param name="service">Коллекция служб.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void ApplicationAuthenticationConfigure(this IServiceCollection service, IConfiguration configuration)
    {
        var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();

        service.AddAuthorization();
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            });
    }

    /// <summary>
    /// Добавляет валидацию приложения.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    public static void AddApplicationValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(LoginModelValidator).Assembly);
    }
}