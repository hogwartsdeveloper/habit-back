using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Infrastructure.Persistence;

namespace Users.Endpoints.Extensions;

/// <summary>
/// Расширение настройки приложения.
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Инициализация модуля пользователей в приложении.
    /// </summary>
    /// <param name="app">Экземпляр приложения, к которому применяется расширение.</param>
    public static async Task UsersModuleInit(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        await app.MigrationAsync();
    }
    
    private static async Task MigrationAsync(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}