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
    [Migration("20191204154743_Updated_c_TasaOCuota")]
    partial class Updated_c_TasaOCuota
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
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Nombre");

                    b.HasKey("Clave");

                    b.ToTable("c_Aduana");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_ClaveProdServ", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("EstimuloFranjaFronteriza");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<int>("IncluirIEPS");

                    b.Property<int>("IncluirIVA");

                    b.Property<string>("Nombre");

                    b.HasKey("Clave");

                    b.ToTable("c_ClaveProdServ");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_ClaveUnidad", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Nombre");

                    b.HasKey("Clave");

                    b.ToTable("c_ClaveUnidad");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_CodigoPostal", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Estado");

                    b.Property<short>("EstimuloFranjaFronteriza");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Localidad");

                    b.Property<string>("Municipio");

                    b.HasKey("Clave");

                    b.ToTable("c_CodigoPostal");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_FormaPago", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bancarizado");

                    b.Property<int>("CuentaBeneficiario");

                    b.Property<int>("CuentaOrdenante");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Nombre");

                    b.Property<int>("NombreBancoEmisorCuentaOrdenante");

                    b.Property<string>("NombreBancoEmisorCuentaOrdenanteRule");

                    b.Property<int>("NumeroOperacion");

                    b.Property<string>("PatronCuentaBeneficiaria");

                    b.Property<string>("PatronCuentaOrdenante");

                    b.Property<int>("RfcEmisorCuentaBeneficiario");

                    b.Property<int>("RfcEmisorCuentaOrdenante");

                    b.Property<int>("TipoCadenaPago");

                    b.HasKey("Clave");

                    b.ToTable("c_FormaPago");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Impuesto", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Entidad");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("LocalOFederal");

                    b.Property<string>("Nombre");

                    b.Property<int>("Retencion");

                    b.Property<int>("Traslado");

                    b.HasKey("Clave");

                    b.ToTable("c_Impuesto");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_MetodoPago", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Nombre");

                    b.HasKey("Clave");

                    b.ToTable("c_MetodoPago");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Moneda", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Decimales");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Nombre");

                    b.Property<short?>("PorcentajeVariacion");

                    b.HasKey("Clave");

                    b.ToTable("c_Moneda");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_NumPedimentoAduana", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Aduana");

                    b.Property<string>("Cantidad");

                    b.Property<string>("Ejercicio");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Patente");

                    b.HasKey("Id");

                    b.ToTable("c_NumPedimentoAduana");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_Pais", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Agrupaciones");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("FormatoCodigoPostal");

                    b.Property<string>("FormatoRegistroIdentidad");

                    b.Property<string>("Nombre");

                    b.Property<string>("ValidacionRegistroIdentidad");

                    b.HasKey("Clave");

                    b.ToTable("c_Pais");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_PatenteAduanal", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.HasKey("Clave");

                    b.ToTable("c_PatenteAduanal");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_RegimenFiscal", b =>
                {
                    b.Property<string>("Clave")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<int>("Fisica");

                    b.Property<int>("Moral");

                    b.Property<string>("Nombre");

                    b.HasKey("Clave");

                    b.ToTable("c_RegimenFiscal");
                });

            modelBuilder.Entity("Labs.Excel.Loader.Model.c_TasaOCuota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Factor");

                    b.Property<DateTime?>("FechaFin");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Impuesto");

                    b.Property<int>("Retencion");

                    b.Property<int>("TasaOCuotaType");

                    b.Property<int>("Traslado");

                    b.Property<string>("ValorMaximo");

                    b.Property<string>("ValorMinimo");

                    b.HasKey("Id");

                    b.ToTable("c_TasaOCuota");
                });
#pragma warning restore 612, 618
        }
    }
}
