using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class NumPedimentoAduanaConfiguration : IEntityTypeConfiguration<c_NumPedimentoAduana>
    {
        public void Configure(EntityTypeBuilder<c_NumPedimentoAduana> builder)
        {
            builder.ToTable("c_NumPedimentoAduana");
            builder.Property(t => t.Id).UseSqlServerIdentityColumn();
            builder.Property(p => p.Aduana).HasMaxLength(2);
            builder.Property(p => p.Patente).HasMaxLength(4);
            builder.Property(p => p.Ejercicio).HasMaxLength(4);
            builder.Property(p => p.Cantidad).HasMaxLength(6);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
