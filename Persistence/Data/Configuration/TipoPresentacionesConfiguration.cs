

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class TipoPresentacionesConfiguration : IEntityTypeConfiguration<TipoPresentacion>
        {
            public void Configure(EntityTypeBuilder<TipoPresentacion> builder)
            {
                builder.ToTable("TIPOS_PRESENTACIONES");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
                
                builder.Property(p=> p.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(75)
                .IsRequired();
            }
        }