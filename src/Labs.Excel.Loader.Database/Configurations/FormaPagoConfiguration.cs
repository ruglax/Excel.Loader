using System;
using System.Collections.Generic;
using System.Text;
using Labs.Excel.Loader.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Excel.Loader.Database.Configurations
{
    public class FormaPagoConfiguration : IEntityTypeConfiguration<c_FormaPago>
    {
        public void Configure(EntityTypeBuilder<c_FormaPago> builder)
        {
            builder.ToTable("c_FormaPago");
            builder.Property(p => p.Id).HasMaxLength(2);
            builder.Property(p => p.Descripcion).HasMaxLength(500);
            builder.Property(p => p.PatronCuentaOrdenante).HasMaxLength(500);
            builder.Property(p => p.PatronCuentaBeneficiaria).HasMaxLength(500);
            builder.Property(p => p.FechaInicio).HasColumnType("date");
            builder.Property(p => p.FechaFin).HasColumnType("date");
        }
    }
}
