
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class RecetaConfiguration : IEntityTypeConfiguration<Receta>
        {
            public void Configure(EntityTypeBuilder<Receta> builder)
            {
                builder.ToTable("Receta");

                builder.Property(p=> p.FechaVencimiento)
                .HasColumnName("Fecha Vencimiento")
                .HasColumnType("datetime")
                .IsRequired();

                builder.HasOne(p => p.Doctor)
                .WithMany(p => p.Recetas)
                .HasForeignKey(p => p.IdDoctorFk);
            }
        }