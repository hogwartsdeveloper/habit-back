using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Auth;

namespace Users.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности RefreshToken.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Настройка сущности RefreshToken.
    /// </summary>
    /// <param name="builder">Построитель сущности.</param>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey("Id");
        builder.ToTable("RefreshTokens");
        builder
            .Property(e => e.Expires)
            .HasColumnType("timestamp with time zone");

        builder
            .HasOne(e => e.User)
            .WithOne(e => e.RefreshToken)
            .HasForeignKey<RefreshToken>(e => e.UserId);
    }
}