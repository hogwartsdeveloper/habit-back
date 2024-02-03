using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class HabitConfiguration : IEntityTypeConfiguration<Habit.Core.Entities.Habit>
{
    public void Configure(EntityTypeBuilder<Habit.Core.Entities.Habit> builder)
    {
        builder.HasKey("Id");
        
        builder.ToTable("Habits");
        
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Habits)
            .HasForeignKey(e => e.UserId);
        
        builder
            .Property(e => e.StartDate)
            .HasColumnType("timestamp without time zone");
        
        builder
            .Property(e => e.EndDate)
            .HasColumnType("timestamp without time zone");
    }
}