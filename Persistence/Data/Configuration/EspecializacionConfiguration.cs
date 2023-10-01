

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class EspecializacionConfiguration : IEntityTypeConfiguration<Especializacion>
        {
            public void Configure(EntityTypeBuilder<Especializacion> builder)
            {
                builder.ToTable("ESPECIALIZACIONES");

                builder.Property(spc=> spc.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(spc => spc.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(50)
                .IsRequired(false);


                



            }
        }