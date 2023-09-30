
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
        {
            public void Configure(EntityTypeBuilder<Estado> builder)
            {
                builder.ToTable("Estado");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();
            }
        }