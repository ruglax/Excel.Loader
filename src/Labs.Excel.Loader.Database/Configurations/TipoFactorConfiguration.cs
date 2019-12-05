using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class TipoFactorConfiguration : IEntityTypeConfiguration<c_TipoFactor>
    {
        public void Configure(EntityTypeBuilder<c_TipoFactor> builder)
        {
            builder.ToTable("c_TipoFactor");
            builder.Property(p => p.Id).HasMaxLength(10);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
