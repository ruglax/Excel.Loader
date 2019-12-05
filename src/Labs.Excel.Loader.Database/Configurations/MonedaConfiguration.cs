using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class MonedaConfiguration : IEntityTypeConfiguration<c_Moneda>
    {
        public void Configure(EntityTypeBuilder<c_Moneda> builder)
        {
            builder.ToTable("c_Moneda").HasKey(p => p.Id);
            builder.Property(p => p.Descripcion).HasMaxLength(100);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
