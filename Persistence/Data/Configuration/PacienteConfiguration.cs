

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
        {
            public void Configure(EntityTypeBuilder<Paciente> builder)
            {
                builder.ToTable("PACIENTES");

                builder.Property(pct=> pct.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(pct=> pct.FechaRegistro)
                .HasColumnName("fecha_registro")
                .HasColumnType("datetime")
                .IsRequired();

                builder.Property(pct=> pct.Documento)
                .HasColumnName("documento")
                .HasMaxLength(20)
                .IsRequired();




                builder.HasOne(pct => pct.TipoDocumento)
                .WithMany(tdcto => tdcto.Pacientes)
                .HasForeignKey(pct => pct.IdTipoDocumentoFk);
            }
        }