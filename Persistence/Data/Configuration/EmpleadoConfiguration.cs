

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
        {
            public void Configure(EntityTypeBuilder<Empleado> builder)
            {
                builder.ToTable("EMpleado");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p=> p.FechaRegistro)
                .HasColumnName("Fecha Registro")
                .HasColumnType("datetime")
                .IsRequired();

                builder.Property(p=> p.Documento)
                .HasColumnName("Documento")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.Cargo)
                .WithMany(p => p.Empleados)
                .HasForeignKey(p => p.IdCargoFk);

                builder.HasOne(p => p.TipoDocumento)
                .WithMany(p => p.Empleados)
                .HasForeignKey(p => p.IdTipoDocumentoFk);

                builder.HasOne(p => p.Direccion)
                .WithMany(p => p.Empleados)
                .HasForeignKey(p => p.IdDireccionFk);

                builder.HasOne(p => p.Contacto)
                .WithMany(p => p.Empleados)
                .HasForeignKey(p => p.IdContactoFk);

                


            }
        }