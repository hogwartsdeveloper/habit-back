using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class UserVerifyConfiguration : IEntityTypeConfiguration<UserVerify>
{
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
            .HasColumnType("timestamp without time zone");
    }
}