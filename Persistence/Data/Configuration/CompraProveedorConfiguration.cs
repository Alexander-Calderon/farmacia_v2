

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class CompraProveedorConfiguration : IEntityTypeConfiguration<CompraProveedor>
        {
            public void Configure(EntityTypeBuilder<CompraProveedor> builder)
            {
                builder.ToTable("CompraProveedor");

                builder.Property(e => e.Cantidad)
                .HasColumnName("Stock")
                .HasColumnType("int")
                .IsRequired();

                builder.Property(e => e.PrecioUnitario)
                .HasColumnName("PrecioUnitario")
                .HasColumnType("decimal(22,2)")
                .IsRequired();

                builder.Property(p=> p.FechaCompra) //
                .HasColumnName("Fecha Compra")
                .HasColumnType("datetime")
                .IsRequired();

                builder.HasOne(p => p.Proveedor)
                .WithMany(p => p.CompraProveedores)
                .HasForeignKey(p => p.IdProveedorFk);

                builder.HasOne(p => p.Medicamento)
                .WithMany(p => p.CompraProveedores)
                .HasForeignKey(p => p.IdMedicamentoFk);

                builder.HasOne(p => p.Empleado)
                .WithMany(p => p.CompraProveedores)
                .HasForeignKey(p => p.IdEmpleadoFk);
            }
        }