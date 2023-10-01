

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class PaisConfiguration : IEntityTypeConfiguration<Pais>
        {
            public void Configure(EntityTypeBuilder<Pais> builder)
            {
                builder.ToTable("PAISES");

                builder.Property(ps=> ps.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

            }
        }