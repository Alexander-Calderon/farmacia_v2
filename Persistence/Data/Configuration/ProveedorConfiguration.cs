

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
        {
            public void Configure(EntityTypeBuilder<Proveedor> builder)
            {
                builder.ToTable("Proveedor");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p=> p.Documento)
                .HasColumnName("Documento")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.TipoDocumento)
                .WithMany(p => p.Proveedores)
                .HasForeignKey(p => p.IdTipoDocumentoFk);

                builder.HasOne(p => p.Contacto)
                .WithMany(p => p.Proveedores)
                .HasForeignKey(p => p.IdContactoFk);

                builder.HasOne(p => p.Direccion)
                .WithMany(p => p.Proveedores)
                .HasForeignKey(p => p.IdDireccionFk);
            }
        }