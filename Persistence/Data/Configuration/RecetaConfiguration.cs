
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class RecetaConfiguration : IEntityTypeConfiguration<Receta>
        {
            public void Configure(EntityTypeBuilder<Receta> builder)
            {
                builder.ToTable("RECETAS");

                builder.Property(rct => rct.FechaEmision)
                .HasColumnName("fecha_emision")
                .HasColumnType("datetime")
                .IsRequired();

                builder.Property(rct => rct.FechaVencimiento)
                .HasColumnName("fecha_vencimiento")
                .HasColumnType("datetime")
                .IsRequired();







                builder.HasOne(rct => rct.Doctor)
                .WithMany(dtr => dtr.Recetas)
                .HasForeignKey(rct => rct.IdDoctorFk);

                builder.HasOne(rct => rct.Paciente)
                .WithMany(pct => pct.Recetas)
                .HasForeignKey(rct => rct.IdPacienteFk);
            }
        }