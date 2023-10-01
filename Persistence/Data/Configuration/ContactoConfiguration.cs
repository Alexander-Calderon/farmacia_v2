
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class ContactoConfiguration : IEntityTypeConfiguration<Contacto>
        {
            public void Configure(EntityTypeBuilder<Contacto> builder)
            {
                builder.ToTable("CONTACTOS");

                builder.Property(ctt=> ctt.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(50)
                .IsRequired();


                

                builder.HasOne(ctt => ctt.TipoContacto)
                .WithMany(tctt => tctt.Contactos)
                .HasForeignKey(ctt => ctt.IdTipoContactoFk);
                
                
                
            }
        }