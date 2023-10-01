

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class TipoContactoConfiguration : IEntityTypeConfiguration<TipoContacto>
        {
            public void Configure(EntityTypeBuilder<TipoContacto> builder)
            {
                builder.ToTable("TIPOS_CONTACTOS");

                builder.Property(tc => tc.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
            }
        }