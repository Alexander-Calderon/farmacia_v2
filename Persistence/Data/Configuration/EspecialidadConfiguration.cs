

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class EspecialidadConfiguration : IEntityTypeConfiguration<Especialidad>
        {
            public void Configure(EntityTypeBuilder<Especialidad> builder)
            {
                builder.ToTable("Especialidad");

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();
            }
        }