using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class PaisConfiguration : IEntityTypeConfiguration<c_Pais>
    {
        public void Configure(EntityTypeBuilder<c_Pais> builder)
        {
            builder.ToTable("c_Pais");
            builder.Property(p => p.Id).HasMaxLength(3);
            builder.Property(p => p.Descripcion).HasMaxLength(250);
            builder.Property(p => p.FormatoCodigoPostal).HasMaxLength(250);
            builder.Property(p => p.FormatoRegistroIdentidad).HasMaxLength(250);
            builder.Property(p => p.ValidacionRegistroIdentidad).HasMaxLength(250);
            builder.Property(p => p.Agrupaciones).HasMaxLength(250);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
