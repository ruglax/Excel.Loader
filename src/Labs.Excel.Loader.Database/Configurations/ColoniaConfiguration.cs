using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class ColoniaConfiguration : IEntityTypeConfiguration<c_Colonia>
    {
        public void Configure(EntityTypeBuilder<c_Colonia> builder)
        {
            builder.ToTable("c_Colonia");
            builder.Property(p => p.Id).HasMaxLength(4);
            builder.Property(p => p.CodigoPostal).HasMaxLength(5).IsRequired();
            builder.Property(p => p.Descripcion).HasMaxLength(500);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
