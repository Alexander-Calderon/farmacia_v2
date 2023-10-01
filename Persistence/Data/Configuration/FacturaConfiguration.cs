

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
        {
            public void Configure(EntityTypeBuilder<Factura> builder)
            {
                builder.ToTable("FACTURAS");

                builder.Property(p=> p.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .HasColumnType("datetime")
                .IsRequired();



                builder.HasOne(fct => fct.Empleado)
                .WithMany(empld => empld.Facturas)
                .HasForeignKey(fct => fct.IdEmpleadoFk);

                builder.HasOne(fct => fct.Paciente)
                .WithMany(pcnt => pcnt.Facturas)
                .HasForeignKey(fct => fct.IdPacienteFk);

                builder.HasOne(fct => fct.Estado)
                .WithMany(estd => estd.Facturas)
                .HasForeignKey(fct => fct.IdEstadoFk);
            }
        }