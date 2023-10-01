

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class CompraProveedorConfiguration : IEntityTypeConfiguration<CompraProveedor>
{
    public void Configure(EntityTypeBuilder<CompraProveedor> builder)
    {
        builder.ToTable("COMPRAS_PROVEEDORES"); //Nombre final de la tabla en la BD.

        /*
            * Mappear a las propiedades de las entidades, las características que tendrán los campos en la bd.
        */
        builder.Property(cprv => cprv.PrecioUnitario)
        .HasColumnName("precio_unitario")
        .HasColumnType("decimal(22,2)")
        .IsRequired();

        builder.Property(cprv => cprv.Cantidad)
        .HasColumnName("cantidad")
        .HasColumnType("int")
        .IsRequired();

        builder.Property(cprv=> cprv.FechaCompra)
        .HasColumnName("fecha_compra")
        .HasColumnType("datetime")
        .IsRequired();


        /*
        * Aquí se usan los objetos de entidades y colecciones de ellas
        * para mappear las relaciones existentes con Ciudad:
        * un departamento a muchas ciudades.
        */

        // LECTURA: Una Única CompraProveedor tiene un #ÚNICO Proveedor
        //                    cprv                     .Proveedor
        // Un Único proveedor tiene #MUCHAS compras.
        //                    prv                      .ComprasProveedores
        // la llave foránea está en la Entidad CompraProveedor y se llama IdProveedorFk
        builder.HasOne(cprv => cprv.Proveedor) // ? objeto que representa la fk en la entidad CompraProveedor
        .WithMany(prv => prv.ComprasProveedores) // ? el collection que tiene la tabla principal para sus referenciadores en las otras tablas con fk del Id de CompraProveedor
        .HasForeignKey(cprv => cprv.IdProveedorFk);

        //
        builder.HasOne(cprv => cprv.Empleado)
        .WithMany(empld => empld.ComprasProveedores)
        .HasForeignKey(cprv => cprv.IdEmpleadoFk);
        
        //
        builder.HasOne(cprv => cprv.Medicamento)
        .WithMany(mdmto => mdmto.ComprasProveedores)
        .HasForeignKey(cprv => cprv.IdMedicamentoFk);


       
    }
}