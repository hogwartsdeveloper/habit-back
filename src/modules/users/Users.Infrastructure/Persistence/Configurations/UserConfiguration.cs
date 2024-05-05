using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Users;

namespace Users.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности User.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Настройка сущности User.
    /// </summary>
    /// <param name="builder">Построитель сущности.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey("Id");
        builder.ToTable("Users");
        builder
            .Property(e => e.BirthDay)
            .HasColumnType("timestamp with time zone");
    }
}