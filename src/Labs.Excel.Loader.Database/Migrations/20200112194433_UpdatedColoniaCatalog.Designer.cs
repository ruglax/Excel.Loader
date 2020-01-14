﻿// <auto-generated />
using System;
using Labs.Excel.Loader.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Labs.Excel.Loader.Database.Migrations
{
    [DbContext(typeof(DbCatalogosContext))]
    [Migration("20200112194433_UpdatedColoniaCatalog")]
    partial class UpdatedColoniaCatalog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Aduana", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("c_Aduana");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_ClaveProdServ", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(8);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<short>("EstimuloFranjaFronteriza");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<int>("IncluirIEPS");

                    b.Property<int>("IncluirIVA");

                    b.HasKey("Id");

                    b.ToTable("c_ClaveProdServ");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_ClaveUnidad", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("c_ClaveUnidad");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_CodigoPostal", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5);

                    b.Property<string>("Estado")
                        .HasMaxLength(3);

                    b.Property<short>("EstimuloFranjaFronteriza");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("Localidad")
                        .HasMaxLength(3);

                    b.Property<string>("Municipio")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.ToTable("c_CodigoPostal");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Colonia", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4);

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(5);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("c_Colonia");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_FormaPago", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2);

                    b.Property<int>("Bancarizado");

                    b.Property<int>("CuentaBeneficiario");

                    b.Property<int>("CuentaOrdenante");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<int>("NombreBancoEmisorCuentaOrdenante");

                    b.Property<string>("NombreBancoEmisorCuentaOrdenanteRule");

                    b.Property<int>("NumeroOperacion");

                    b.Property<string>("PatronCuentaBeneficiaria")
                        .HasMaxLength(500);

                    b.Property<string>("PatronCuentaOrdenante")
                        .HasMaxLength(500);

                    b.Property<int>("RfcEmisorCuentaBeneficiario");

                    b.Property<int>("RfcEmisorCuentaOrdenante");

                    b.Property<int>("TipoCadenaPago");

                    b.HasKey("Id");

                    b.ToTable("c_FormaPago");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Impuesto", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<string>("Entidad");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("LocalOFederal");

                    b.Property<int>("Retencion");

                    b.Property<int>("Traslado");

                    b.HasKey("Id");

                    b.ToTable("c_Impuesto");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_MetodoPago", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("c_MetodoPago");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Moneda", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Decimales");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<short?>("PorcentajeVariacion");

                    b.HasKey("Id");

                    b.ToTable("c_Moneda");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_NumPedimentoAduana", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Aduana")
                        .HasMaxLength(2);

                    b.Property<string>("Cantidad")
                        .HasMaxLength(6);

                    b.Property<string>("Ejercicio")
                        .HasMaxLength(4);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("Patente")
                        .HasMaxLength(4);

                    b.HasKey("Id");

                    b.ToTable("c_NumPedimentoAduana");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Pais", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<string>("Agrupaciones")
                        .HasMaxLength(250);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("FormatoCodigoPostal")
                        .HasMaxLength(250);

                    b.Property<string>("FormatoRegistroIdentidad")
                        .HasMaxLength(250);

                    b.Property<string>("ValidacionRegistroIdentidad")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("c_Pais");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_PatenteAduanal", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("c_PatenteAduanal");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_RegimenFiscal", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<int>("Fisica");

                    b.Property<int>("Moral");

                    b.HasKey("Id");

                    b.ToTable("c_RegimenFiscal");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_TasaOCuota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Factor");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("Impuesto")
                        .HasMaxLength(4);

                    b.Property<int>("Retencion");

                    b.Property<int>("TasaOCuotaType");

                    b.Property<int>("Traslado");

                    b.Property<string>("ValorMaximo")
                        .HasMaxLength(18);

                    b.Property<string>("ValorMinimo")
                        .HasMaxLength(18);

                    b.HasKey("Id");

                    b.ToTable("c_TasaOCuota");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_TipoDeComprobante", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<string>("ValorMaximo")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("c_TipoDeComprobante");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_TipoFactor", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("c_TipoFactor");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_TipoRelacion", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("c_TipoRelacion");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_UsoCFDI", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<string>("Descripcion")
                        .HasMaxLength(250);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<int>("Fisica");

                    b.Property<int>("Moral");

                    b.HasKey("Id");

                    b.ToTable("c_UsoCFDI");
                });
#pragma warning restore 612, 618
        }
    }
}
