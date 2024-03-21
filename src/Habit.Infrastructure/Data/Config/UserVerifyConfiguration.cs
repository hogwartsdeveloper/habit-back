using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

/// <summary>
/// Конфигурация сущности UserVerify.
/// </summary>
public class UserVerifyConfiguration : IEntityTypeConfiguration<UserVerify>
{
    /// <summary>
    /// Настройка сущности UserVerify.
    /// </summary>
    /// <param name="builder">Построитель сущности.</param>
    public void Configure(EntityTypeBuilder<UserVerify> builder)
    {
        builder.HasKey("Id");
        builder.ToTable("UserVerification");
        
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.UserVerification)
            .HasForeignKey(e => e.UserId);

        builder.HasIndex(e => e.Exp);
        
        builder
            .Property(e => e.Exp)
            .HasColumnType("timestamp with time zone");
    }
}