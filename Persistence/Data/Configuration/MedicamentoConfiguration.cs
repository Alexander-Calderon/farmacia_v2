

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
        {
            public void Configure(EntityTypeBuilder<Medicamento> builder)
            {
                builder.ToTable("MEDICAMENTOS");

                builder.Property(mdmto => mdmto.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsRequired();

                builder.Property(mdmto => mdmto.Descripcion)
                .HasColumnName("descripcion")
                .HasMaxLength(75)
                .IsRequired();

                builder.Property(mdmto => mdmto.PrecioUnitario)
                .HasColumnName("precio_unitario")
                .HasColumnType("decimal(22,2)")
                .IsRequired();

                builder.Property(mdmto => mdmto.Stock)
                .HasColumnName("stock")
                .HasColumnType("int")
                .IsRequired();

                builder.Property(mdmto => mdmto.FechaVencimiento)
                .HasColumnName("fecha_vencimiento")
                .HasColumnType("datetime")
                .IsRequired();





                

                builder.HasOne(mdmto => mdmto.Categoria)
                .WithMany(ctg => ctg.Medicamentos)
                .HasForeignKey(mdmto => mdmto.IdMarcaFk);

                builder.HasOne(mdmto => mdmto.TipoPresentacion)
                .WithMany(tptc => tptc.Medicamentos)
                .HasForeignKey(mdmto => mdmto.IdTipoPresentacionFk);

                builder.HasOne(mdmto => mdmto.Marca)
                .WithMany(mrc => mrc.Medicamentos)
                .HasForeignKey(mdmto => mdmto.IdMarcaFk);
            }
        }