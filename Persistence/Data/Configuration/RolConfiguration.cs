

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
        {
            public void Configure(EntityTypeBuilder<Rol> builder)
            {
                builder.ToTable("ROLES");
    
                builder.Property(rl => rl.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
            }
        }