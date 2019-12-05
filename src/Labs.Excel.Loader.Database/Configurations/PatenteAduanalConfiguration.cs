using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class PatenteAduanalConfiguration : IEntityTypeConfiguration<c_PatenteAduanal>
    {
        public void Configure(EntityTypeBuilder<c_PatenteAduanal> builder)
        {
            builder.ToTable("c_PatenteAduanal");
            builder.Property(p => p.Id).HasMaxLength(4);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
