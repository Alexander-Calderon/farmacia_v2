

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ContactoDetalleConfiguration : IEntityTypeConfiguration<ContactoDetalle>
        {
            public void Configure(EntityTypeBuilder<ContactoDetalle> builder)
            {
                builder.ToTable("ContactoDetalle");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.Contacto)
                .WithMany(p => p.ContactoDetalles)
                .HasForeignKey(p => p.IdTipoContactoFk);

                builder.HasOne(p => p.TipoContacto)
                .WithMany(p => p.ContactoDetalles)
                .HasForeignKey(p => p.IdTipoContactoFk);

            }
        }