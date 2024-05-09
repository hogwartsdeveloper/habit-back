using Habits.Domain.Habits;
using Habits.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Habits.Infrastructure.Persistence;

/// <summary>
/// Контекст базы данных Habits.
/// </summary>
public class HabitsDbContext : DbContext
{
    /// <summary>
    /// Набор данных привычек.
    /// </summary>
    public DbSet<Domain.Habits.Habit> Habits { get; set; }
    
    /// <summary>
    /// Набор данных записей о привычках.
    /// </summary>
    public DbSet<HabitRecord> HabitRecords { get; set; }

    public HabitsDbContext(DbContextOptions<HabitsDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Настройка модели базы данных приложения.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("habits");
        modelBuilder.ApplyConfiguration(new HabitConfiguration());
        modelBuilder.ApplyConfiguration(new HabitRecordConfiguration());
    }
}