
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ContactoProveedorConfiguration : IEntityTypeConfiguration<ContactoProveedor>
        {
            public void Configure(EntityTypeBuilder<ContactoProveedor> builder)
            {
                builder.ToTable("CONTACTOS_PROVEEDORES");

                
                builder.HasKey(cprv => new { cprv.IdContactoFk, cprv.IdProveedorFk });

                builder.HasOne(cprv => cprv.Contacto)
                .WithMany(ctt => ctt.ContactosProveedores)
                .HasForeignKey(cprv => cprv.IdContactoFk);

                builder.HasOne(cprv => cprv.Proveedor)
                .WithMany(prv => prv.ContactosProveedores)
                .HasForeignKey(cprv => cprv.IdProveedorFk);
                
                
                
            }
        }