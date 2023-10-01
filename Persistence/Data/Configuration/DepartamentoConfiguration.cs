

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
        {
            public void Configure(EntityTypeBuilder<Departamento> builder)
            {
                builder.ToTable("DEPARTAMENTOS");

                builder.Property(dpto=> dpto.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(dpto => dpto.Pais)
                .WithMany(ps => ps.Departamentos)
                .HasForeignKey(dpto => dpto.IdPaisFk); 
            }
        }