

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
        {
            public void Configure(EntityTypeBuilder<Doctor> builder)
            {
                builder.ToTable("DOCTORES");

                builder.Property(dtr=> dtr.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(dtr=> dtr.FechaRegistro)
                .HasColumnName("fecha_registro")
                .HasColumnType("datetime")
                .IsRequired();

                builder.Property(dtr => dtr.Documento)
                .HasColumnName("documento")
                .HasMaxLength(20)
                .IsRequired();





                builder.HasOne(dtr => dtr.TipoDocumento)
                .WithMany(tdcto => tdcto.Doctores)
                .HasForeignKey(dtr => dtr.IdTipoDocumentoFk);

                builder.HasOne(dtr => dtr.Especializacion)
                .WithMany(spc => spc.Doctores)
                .HasForeignKey(dtr => dtr.IdEspecialidadFk);
                
            }
        }