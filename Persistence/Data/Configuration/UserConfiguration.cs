

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.ToTable("Users");
    
                builder.Property(p=> p.UserName)
                .HasColumnName("UserName")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p=> p.Password)
                .HasColumnName("Password")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.Rol)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.IdRolFk);
            }
        }