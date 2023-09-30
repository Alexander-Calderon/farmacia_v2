

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
        {
            public void Configure(EntityTypeBuilder<Medicamento> builder)
            {
                builder.ToTable("Medicamento");

                builder.Property(p=> p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(p=> p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(e => e.PrecioUnitario)
                .HasColumnName("PrecioUnitario")
                .HasColumnType("decimal(22,2)")
                .IsRequired();

                builder.Property(e => e.Stock)
                .HasColumnName("Stock")
                .HasColumnType("int")
                .IsRequired();

                builder.Property(p=> p.FechaVencimiento)
                .HasColumnName("Fecha Vencimiento")
                .HasColumnType("datetime")
                .IsRequired();

                builder.HasOne(p => p.Categoria)
                .WithMany(p => p.Medicamentos)
                .HasForeignKey(p => p.IdMarcaFk);

                builder.HasOne(p => p.TipoPresentacion)
                .WithMany(p => p.Medicamentos)
                .HasForeignKey(p => p.IdTipoPresentacionFk);

                builder.HasOne(p => p.Marca)
                .WithMany(p => p.Medicamentos)
                .HasForeignKey(p => p.IdMarcaFk);
            }
        }