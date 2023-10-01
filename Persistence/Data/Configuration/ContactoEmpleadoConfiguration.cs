
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ContactoEmpleadoConfiguration : IEntityTypeConfiguration<ContactoEmpleado>
        {
            public void Configure(EntityTypeBuilder<ContactoEmpleado> builder)
            {
                builder.ToTable("CONTACTOS_EMPLEADOS");

                
                builder.HasKey(cemp => new { cemp.IdContactoFk, cemp.IdEmpleadoFk });

                builder.HasOne(cemp => cemp.Contacto)
                .WithMany(ctt => ctt.ContactosEmpleados)
                .HasForeignKey(cemp => cemp.IdContactoFk);

                builder.HasOne(cemp => cemp.Empleado)
                .WithMany(emp => emp.ContactosEmpleados)
                .HasForeignKey(cemp => cemp.IdEmpleadoFk);
                
                
                
            }
        }