
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ContactoConfiguration : IEntityTypeConfiguration<Contacto>
        {
            public void Configure(EntityTypeBuilder<Contacto> builder)
            {
                builder.ToTable("Contacto");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();
                
                
            }
        }