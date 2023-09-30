

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class PaisConfiguration : IEntityTypeConfiguration<Pais>
        {
            public void Configure(EntityTypeBuilder<Pais> builder)
            {
                builder.ToTable("Pais");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

            }
        }