

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
        {
            public void Configure(EntityTypeBuilder<Ciudad> builder)
            {
                builder.ToTable("Ciudad");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.Departamento)
                .WithMany(p => p.Ciudades)
                .HasForeignKey(p => p.IdDepartamentoFK); 
            }
        }