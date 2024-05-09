using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habits.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности Habit.
/// </summary>
public class HabitConfiguration : IEntityTypeConfiguration<Domain.Habits.Habit>
{
    /// <summary>
    /// Настройка сущности Habit.
    /// </summary>
    /// <param name="builder">Построитель сущности.</param>
    public void Configure(EntityTypeBuilder<Domain.Habits.Habit> builder)
    {
        builder.HasKey("Id");
        
        builder.ToTable("Habits");
        
        builder
            .Property(e => e.StartDate)
            .HasColumnType("timestamp without time zone");
        
        builder
            .Property(e => e.EndDate)
            .HasColumnType("timestamp without time zone");
    }
}