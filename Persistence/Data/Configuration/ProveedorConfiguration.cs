

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
        {
            public void Configure(EntityTypeBuilder<Proveedor> builder)
            {
                builder.ToTable("PROVEEDORES");

                builder.Property(prv=> prv.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(prv=> prv.Documento)
                .HasColumnName("documento")
                .HasMaxLength(20)
                .IsRequired();




                builder.HasOne(prv => prv.TipoDocumento)
                .WithMany(tdcto => tdcto.Proveedores)
                .HasForeignKey(prv => prv.IdTipoDocumentoFk);

                builder.HasOne(prv => prv.Direccion)
                .WithMany(dccn => dccn.Proveedores)
                .HasForeignKey(prv => prv.IdDireccionFk);
            }
        }