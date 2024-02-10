using Habit.Domain.Entities;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Habit.Domain.Entities.Habit> Habits { get; set; }
    
    public DbSet<HabitRecord> HabitRecords { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new HabitConfiguration());
        modelBuilder.ApplyConfiguration(new HabitRecordConfiguration());
    }
}