

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DetalleFacturaConfiguration : IEntityTypeConfiguration<DetalleFactura>
        {
            public void Configure(EntityTypeBuilder<DetalleFactura> builder)
            {
                builder.ToTable("DETALLES_FACTURAS");
    
                builder.Property(dfct => dfct.PrecioUnitario)
                .HasColumnName("precio_unitario")
                .HasColumnType("decimal(22,2)")
                .IsRequired();

                builder.Property(dfct => dfct.Cantidad)
                .HasColumnName("cantidad")
                .HasColumnType("int")
                .IsRequired();



                builder.HasOne(dfct => dfct.Factura)
                .WithMany(fct => fct.DetallesFacturas)
                .HasForeignKey(dfct => dfct.IdFacturaFk);

                builder.HasOne(dfct => dfct.Medicamento)
                .WithMany(mdmto => mdmto.DetallesFacturas)
                .HasForeignKey(dfct => dfct.IdMedicamentoFk);

                builder.HasOne(dfct => dfct.Receta)
                .WithMany(rct => rct.DetallesFacturas)
                .HasForeignKey(dfct => dfct.IdRecetaFk);

                
            }
        }