using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class ClaveUnidadConfiguration : IEntityTypeConfiguration<c_ClaveUnidad>
    {
        public void Configure(EntityTypeBuilder<c_ClaveUnidad> builder)
        {
            builder.ToTable("c_ClaveUnidad");
            builder.Property(p => p.Id).HasMaxLength(3);
            builder.Property(p => p.Descripcion).HasMaxLength(500);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
