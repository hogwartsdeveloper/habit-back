using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitServer.Entities.Configurations;

public class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
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