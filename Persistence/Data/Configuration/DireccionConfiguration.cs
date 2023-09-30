

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
        {
            public void Configure(EntityTypeBuilder<Direccion> builder)
            {
                builder.ToTable("Ciudad");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.Ciudad)
                .WithMany(p => p.Direcciones)
                .HasForeignKey(p => p.IdCiudadFk); 
    
            }
        }