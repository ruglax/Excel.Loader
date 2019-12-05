using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class TipoDeComprobanteConfiguration : IEntityTypeConfiguration<c_TipoDeComprobante>
    {
        public void Configure(EntityTypeBuilder<c_TipoDeComprobante> builder)
        {
            builder.ToTable("c_TipoDeComprobante");
            builder.Property(p => p.Id).HasMaxLength(1);
            builder.Property(p => p.Descripcion).HasMaxLength(100);
            builder.Property(p => p.ValorMaximo).HasMaxLength(100);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
