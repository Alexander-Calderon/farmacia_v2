

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DetalleFacturaConfiguration : IEntityTypeConfiguration<DetalleFactura>
        {
            public void Configure(EntityTypeBuilder<DetalleFactura> builder)
            {
                builder.ToTable("DetalleFactura");
    
                builder.Property(e => e.PrecioVenta)
                .HasColumnName("precio")
                .HasColumnType("decimal(22,2)")
                .IsRequired();

                builder.HasOne(p => p.Factura)
                .WithMany(p => p.DetalleFacturas)
                .HasForeignKey(p => p.IdFacturaFk);

                builder.HasOne(p => p.Medicamento)
                .WithMany(p => p.DetalleFacturas)
                .HasForeignKey(p => p.IdMedicamentoFk);

                
            }
        }