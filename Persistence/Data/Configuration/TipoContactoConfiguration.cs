

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class TipoContactoConfiguration : IEntityTypeConfiguration<TipoContacto>
        {
            public void Configure(EntityTypeBuilder<TipoContacto> builder)
            {
                builder.ToTable("TipoContacto");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();
            }
        }