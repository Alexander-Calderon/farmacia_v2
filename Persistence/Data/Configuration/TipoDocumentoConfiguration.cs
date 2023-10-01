
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
        {
            public void Configure(EntityTypeBuilder<TipoDocumento> builder)
            {
                builder.ToTable("TIPOS_DOCUMENTOS");

                builder.Property(p=> p.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();
            }
        }