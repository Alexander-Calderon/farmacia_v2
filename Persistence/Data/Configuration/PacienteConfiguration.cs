

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
        {
            public void Configure(EntityTypeBuilder<Paciente> builder)
            {
                builder.ToTable("Paciente");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p=> p.FechaRegistro)
                .HasColumnName("Fecha Registro")
                .HasColumnType("datetime")
                .IsRequired();

                builder.Property(p=> p.Documento)
                .HasColumnName("Documento")
                .HasMaxLength(50)
                .IsRequired();

                builder.HasOne(p => p.TipoDocumento)
                .WithMany(p => p.Pacientes)
                .HasForeignKey(p => p.IdTipoDocumentoFk);
            }
        }