using System.Text;
using Habit.Application.Auth;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Services;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Services;
using Habit.Application.Mail.Interfaces;
using Habit.Application.Mail.Models;
using Habit.Application.Mail.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Habit.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAutoMapper(typeof(AuthMapperProfile).Assembly);
        service.AddSingleton<ISecurityService, SecurityService>();
        service.AddScoped<IAuthService, AuthService>();
        service.AddScoped<IHabitService, HabitService>();
        service.AddSingleton<IMailService, MailService>();
        service.Configure<MailSettings>(configuration.GetSection("MailSettings"));
    }

    public static void ApplicationAuthenticationConfigure(this IServiceCollection service, IConfiguration configuration)
    {
        var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
    }
}