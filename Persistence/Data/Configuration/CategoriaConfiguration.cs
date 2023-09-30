

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
        {
            public void Configure(EntityTypeBuilder<Categoria> builder)
            {
                builder.ToTable("Categoria");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();
            }
        }