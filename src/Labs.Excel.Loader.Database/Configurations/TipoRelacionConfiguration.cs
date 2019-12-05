using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class TipoRelacionConfiguration : IEntityTypeConfiguration<c_TipoRelacion>
    {
        public void Configure(EntityTypeBuilder<c_TipoRelacion> builder)
        {
            builder.ToTable("c_TipoRelacion");
            builder.Property(p => p.Id).HasMaxLength(2);
            builder.Property(p => p.Descripcion).HasMaxLength(250);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
