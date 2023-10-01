

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("CIUDADES");

        /*
            * Mappear a las propiedades de las entidades, las características que tendrán los campos en la bd.
        */
        builder.Property( cdd=> cdd.Nombre)
        .HasColumnName("nombre")
        .HasMaxLength(50)
        .IsRequired();



        /*
        * Aquí se usan los objetos de entidades y colecciones de ellas
        * para mappear las relaciones existentes con Ciudad:
        * un departamento a muchas ciudades.
        */
        builder.HasOne(cdd => cdd.Departamento)
        .WithMany(dpto => dpto.Ciudades)
        .HasForeignKey(cdd => cdd.IdDepartamentoFK);

        
    }
}