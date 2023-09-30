

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class CargoConfiguration : IEntityTypeConfiguration<Cargo>
        {
            public void Configure(EntityTypeBuilder<Cargo> builder)
            {
                builder.ToTable("cargos");

                builder.Property(p=> p.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
            }
        }