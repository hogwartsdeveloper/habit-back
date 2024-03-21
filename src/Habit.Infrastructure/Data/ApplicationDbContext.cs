using Habit.Domain.Entities;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// Контекст базы данных приложения.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Набор данных пользователей.
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Набор данных привычек.
    /// </summary>
    public DbSet<Habit.Domain.Entities.Habit> Habits { get; set; }
    
    /// <summary>
    /// Набор данных записей о привычках.
    /// </summary>
    public DbSet<HabitRecord> HabitRecords { get; set; }
    
    /// <summary>
    /// Набор данных обновлений токенов доступа.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Конструктор контекста базы данных приложения.
    /// </summary>
    /// <param name="options">Настройки контекста базы данных.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Настройка модели базы данных приложения.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new HabitConfiguration());
        modelBuilder.ApplyConfiguration(new HabitRecordConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
    }
}