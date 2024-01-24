using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitServer.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey("Id");
        builder.ToTable("Users");
        builder
            .Property(e => e.BirthDay)
            .HasColumnType("timestamp without time zone");
    }
}