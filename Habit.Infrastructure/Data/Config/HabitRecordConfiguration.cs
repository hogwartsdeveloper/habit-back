using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class HabitRecordConfiguration : IEntityTypeConfiguration<HabitRecord>
{
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