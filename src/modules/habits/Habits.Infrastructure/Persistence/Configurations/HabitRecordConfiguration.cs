using Habits.Domain.Habits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habits.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности HabitRecord.
/// </summary>
public class HabitRecordConfiguration : IEntityTypeConfiguration<HabitRecord>
{
    /// <summary>
    /// Настройка сущности HabitRecord.
    /// </summary>
    /// <param name="builder">Построитель сущности.</param>
    public void Configure(EntityTypeBuilder<HabitRecord> builder)
    {
        builder.HasKey("Id");

        builder.ToTable("HabitRecords");

        builder
            .HasOne(e => e.Habit)
            .WithMany(h => h.HabitRecords)
            .HasForeignKey(r => r.HabitId);

        builder.HasIndex(r => r.Date);

        builder
            .Property(r => r.Date)
            .HasColumnType("timestamp without time zone");
    }
}