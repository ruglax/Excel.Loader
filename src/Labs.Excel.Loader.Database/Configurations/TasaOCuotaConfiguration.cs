using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class TasaOCuotaConfiguration : IEntityTypeConfiguration<c_TasaOCuota>
    {
        public void Configure(EntityTypeBuilder<c_TasaOCuota> builder)
        {
            builder.ToTable("c_TasaOCuota");
            builder.Property(t => t.Id).UseSqlServerIdentityColumn();
            builder.Property(t => t.ValorMinimo).HasMaxLength(18);
            builder.Property(t => t.ValorMaximo).HasMaxLength(18);
            builder.Property(t => t.Impuesto).HasMaxLength(4);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
