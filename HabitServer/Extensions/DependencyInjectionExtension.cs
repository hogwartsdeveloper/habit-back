using System.Text;
using HabitServer.Entities;
using HabitServer.MapperProfiles;
using HabitServer.Services;
using HabitServer.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

    public static void ApplicationConfigureServices(this IServiceCollection service, IConfiguration configuration)
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
        service.AddAutoMapper(typeof(RegistrationViewModelProfile).Assembly);
        service.AddSingleton<ISecurityService, SecurityService>();
        service.AddScoped<IAuthService, AuthService>();
    }
}