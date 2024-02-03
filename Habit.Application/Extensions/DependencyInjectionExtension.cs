using System.Text;
using Habit.Application.Auth;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Services;
using Habit.Application.Habit.Interfaces;
using Habit.Application.Habit.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Habit.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddApplicationServices(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(AuthMapperProfile).Assembly);
        service.AddSingleton<ISecurityService, SecurityService>();
        service.AddScoped<IAuthService, AuthService>();
        service.AddScoped<IHabitService, HabitService>();
    }

    public static void ApplicationAuthenticationConfigure(this IServiceCollection service, IConfiguration configuration)
    {
        var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();
        
        service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
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