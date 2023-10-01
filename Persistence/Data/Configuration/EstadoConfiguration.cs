
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
        {
            public void Configure(EntityTypeBuilder<Estado> builder)
            {
                builder.ToTable("ESTADOS");

                builder.Property(estd=> estd.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
            }
        }