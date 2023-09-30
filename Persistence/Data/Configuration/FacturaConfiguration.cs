

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
        {
            public void Configure(EntityTypeBuilder<Factura> builder)
            {
                builder.ToTable("Factura");

                builder.Property(p=> p.FechaCreacion)
                .HasColumnName("Fecha Creacion")
                .HasColumnType("datetime")
                .IsRequired();

                builder.HasOne(p => p.Empleado)
                .WithMany(p => p.Facturas)
                .HasForeignKey(p => p.IdEmpleadoFk);

                builder.HasOne(p => p.Paciente)
                .WithMany(p => p.Facturas)
                .HasForeignKey(p => p.IdPacienteFk);

                builder.HasOne(p => p.Estado)
                .WithMany(p => p.Facturas)
                .HasForeignKey(p => p.IdEstadoFk);
            }
        }