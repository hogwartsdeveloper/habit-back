using Microsoft.EntityFrameworkCore;
using Users.Domain.Auth;
using Users.Domain.Users;
using Users.Infrastructure.Persistence.Configurations;

namespace Users.Infrastructure.Persistence;

/// <summary>
/// Контекст базы данных для User.
/// </summary>
public class UsersDbContext : DbContext
{
    /// <summary>
    /// Набор данных пользователей.
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Набор данных обновлений токенов доступа.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    /// <summary>
    /// Набор данных для верификации пользователей.
    /// </summary>
    public DbSet<UserVerify> UserVerifications { get; set; }

    /// <summary>
    /// Конструктор контекста базы данных с параметрами конфигурации.
    /// </summary>
    /// <param name="options">Опции конфигурации для контекста базы данных.</param>
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserVerifyConfiguration());
    }
}