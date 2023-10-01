

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
        {
            public void Configure(EntityTypeBuilder<Direccion> builder)
            {
                builder.ToTable("DIRECCIONES");

                builder.Property(dccn=> dccn.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(50)
                .IsRequired();




                builder.HasOne(dccn => dccn.Ciudad)
                .WithMany(cdd => cdd.Direcciones)
                .HasForeignKey(dccn => dccn.IdCiudadFk); 

            }
        }