

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
        {
            public void Configure(EntityTypeBuilder<Departamento> builder)
            {
                builder.ToTable("Departamento");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.Pais)
                .WithMany(p => p.Departamentos)
                .HasForeignKey(p => p.IdPaisFk); 
            }
        }