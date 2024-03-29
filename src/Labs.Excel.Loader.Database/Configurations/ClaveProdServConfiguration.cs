﻿using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class ClaveProdServConfiguration : IEntityTypeConfiguration<c_ClaveProdServ>
    {
        public void Configure(EntityTypeBuilder<c_ClaveProdServ> builder)
        {
            builder.ToTable("c_ClaveProdServ");
            builder.Property(p => p.Id).HasMaxLength(8);
            builder.Property(p => p.Descripcion).HasMaxLength(500);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
