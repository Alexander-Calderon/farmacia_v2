

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
        {
            public void Configure(EntityTypeBuilder<Marca> builder)
            {
                builder.ToTable("Marca");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();
            }
        }