using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class CodigoPostalConfiguration : IEntityTypeConfiguration<c_CodigoPostal>
    {
        public void Configure(EntityTypeBuilder<c_CodigoPostal> builder)
        {
            builder.ToTable("c_CodigoPostal");
            builder.Property(p => p.Id).HasMaxLength(5);
            builder.Property(p => p.Estado).HasMaxLength(3);
            builder.Property(p => p.Municipio).HasMaxLength(3);
            builder.Property(p => p.Localidad).HasMaxLength(3);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
