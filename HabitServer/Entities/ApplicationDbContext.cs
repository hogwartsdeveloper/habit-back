using HabitServer.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HabitServer.Entities;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Habit> Habits { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new HabitConfiguration());
    }
}