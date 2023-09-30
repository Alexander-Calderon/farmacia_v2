

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
        {
            public void Configure(EntityTypeBuilder<Doctor> builder)
            {
                builder.ToTable("Doctor");

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
                .WithMany(p => p.Doctores)
                .HasForeignKey(p => p.IdTipoDocumentoFk);

                builder.HasOne(p => p.Especialidad)
                .WithMany(p => p.Doctores)
                .HasForeignKey(p => p.IdEspecialidadFk);
                
            }
        }