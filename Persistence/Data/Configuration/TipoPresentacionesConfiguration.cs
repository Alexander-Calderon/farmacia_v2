

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class TipoPresentacionesConfiguration : IEntityTypeConfiguration<TipoPresentacion>
        {
            public void Configure(EntityTypeBuilder<TipoPresentacion> builder)
            {
                builder.ToTable("TipoPresentacion");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();
            }
        }