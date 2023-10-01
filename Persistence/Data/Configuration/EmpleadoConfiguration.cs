

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
        {
            public void Configure(EntityTypeBuilder<Empleado> builder)
            {
                builder.ToTable("EMPLEADOS");

                builder.Property( empld=> empld.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(empld => empld.FechaRegistro)
                .HasColumnName("fecha_registro")
                .HasColumnType("datetime")
                .IsRequired();

                builder.Property(empld => empld.Documento)
                .HasColumnName("documento")
                .HasMaxLength(20)
                .IsRequired();








                // * SE MAPPEAN LAS FK DE ESTA TABLA

                builder.HasOne(empld => empld.Cargo)
                .WithMany(crg => crg.Empleados)
                .HasForeignKey(empld => empld.IdCargoFk);

                builder.HasOne(empld => empld.TipoDocumento)
                .WithMany(tdcto => tdcto.Empleados)
                .HasForeignKey(empld => empld.IdTipoDocumentoFk);

                builder.HasOne(empld => empld.Direccion)
                .WithMany(dccn => dccn.Empleados)
                .HasForeignKey(empld => empld.IdDireccionFk);

                // ! Relación de uno a uno
                builder.HasOne(empld => empld.User)
                .WithOne(usr => usr.Empleado)
                .HasForeignKey<Empleado>(empld => empld.IdUserFk); 

                


            }
        }