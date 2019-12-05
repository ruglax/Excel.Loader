using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_Aduana",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 2, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Aduana", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_ClaveProdServ",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 8, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true),
                    IncluirIVA = table.Column<int>(nullable: false),
                    IncluirIEPS = table.Column<int>(nullable: false),
                    EstimuloFranjaFronteriza = table.Column<short>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_ClaveProdServ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_ClaveUnidad",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_ClaveUnidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_CodigoPostal",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 5, nullable: false),
                    Estado = table.Column<string>(maxLength: 3, nullable: true),
                    Municipio = table.Column<string>(maxLength: 3, nullable: true),
                    Localidad = table.Column<string>(maxLength: 3, nullable: true),
                    EstimuloFranjaFronteriza = table.Column<short>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_CodigoPostal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_FormaPago",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 2, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true),
                    Bancarizado = table.Column<int>(nullable: false),
                    NumeroOperacion = table.Column<int>(nullable: false),
                    RfcEmisorCuentaOrdenante = table.Column<int>(nullable: false),
                    CuentaOrdenante = table.Column<int>(nullable: false),
                    PatronCuentaOrdenante = table.Column<string>(maxLength: 500, nullable: true),
                    RfcEmisorCuentaBeneficiario = table.Column<int>(nullable: false),
                    CuentaBeneficiario = table.Column<int>(nullable: false),
                    PatronCuentaBeneficiaria = table.Column<string>(maxLength: 500, nullable: true),
                    TipoCadenaPago = table.Column<int>(nullable: false),
                    NombreBancoEmisorCuentaOrdenante = table.Column<int>(nullable: false),
                    NombreBancoEmisorCuentaOrdenanteRule = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_FormaPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_Impuesto",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true),
                    Retencion = table.Column<int>(nullable: false),
                    Traslado = table.Column<int>(nullable: false),
                    LocalOFederal = table.Column<string>(nullable: true),
                    Entidad = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Impuesto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_MetodoPago",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_MetodoPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_Moneda",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: true),
                    Decimales = table.Column<short>(nullable: false),
                    PorcentajeVariacion = table.Column<short>(nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Moneda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_NumPedimentoAduana",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aduana = table.Column<string>(nullable: true),
                    Patente = table.Column<string>(maxLength: 4, nullable: true),
                    Ejercicio = table.Column<string>(maxLength: 4, nullable: true),
                    Cantidad = table.Column<string>(maxLength: 6, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_NumPedimentoAduana", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_Pais",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 250, nullable: true),
                    FormatoCodigoPostal = table.Column<string>(maxLength: 250, nullable: true),
                    FormatoRegistroIdentidad = table.Column<string>(maxLength: 250, nullable: true),
                    ValidacionRegistroIdentidad = table.Column<string>(maxLength: 250, nullable: true),
                    Agrupaciones = table.Column<string>(maxLength: 250, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_PatenteAduanal",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 4, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_PatenteAduanal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_RegimenFiscal",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true),
                    Fisica = table.Column<int>(nullable: false),
                    Moral = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_RegimenFiscal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_TasaOCuota",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TasaOCuotaType = table.Column<int>(nullable: false),
                    ValorMinimo = table.Column<string>(maxLength: 18, nullable: true),
                    ValorMaximo = table.Column<string>(maxLength: 18, nullable: true),
                    Impuesto = table.Column<string>(maxLength: 4, nullable: true),
                    Factor = table.Column<string>(nullable: true),
                    Traslado = table.Column<int>(nullable: false),
                    Retencion = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_TasaOCuota", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_TipoDeComprobante",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 1, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: true),
                    ValorMaximo = table.Column<string>(maxLength: 100, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_TipoDeComprobante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_TipoFactor",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 10, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_TipoFactor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_TipoRelacion",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 2, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 250, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_TipoRelacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "c_UsoCFDI",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 250, nullable: true),
                    Fisica = table.Column<int>(nullable: false),
                    Moral = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_UsoCFDI", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_Aduana");

            migrationBuilder.DropTable(
                name: "c_ClaveProdServ");

            migrationBuilder.DropTable(
                name: "c_ClaveUnidad");

            migrationBuilder.DropTable(
                name: "c_CodigoPostal");

            migrationBuilder.DropTable(
                name: "c_FormaPago");

            migrationBuilder.DropTable(
                name: "c_Impuesto");

            migrationBuilder.DropTable(
                name: "c_MetodoPago");

            migrationBuilder.DropTable(
                name: "c_Moneda");

            migrationBuilder.DropTable(
                name: "c_NumPedimentoAduana");

            migrationBuilder.DropTable(
                name: "c_Pais");

            migrationBuilder.DropTable(
                name: "c_PatenteAduanal");

            migrationBuilder.DropTable(
                name: "c_RegimenFiscal");

            migrationBuilder.DropTable(
                name: "c_TasaOCuota");

            migrationBuilder.DropTable(
                name: "c_TipoDeComprobante");

            migrationBuilder.DropTable(
                name: "c_TipoFactor");

            migrationBuilder.DropTable(
                name: "c_TipoRelacion");

            migrationBuilder.DropTable(
                name: "c_UsoCFDI");
        }
    }
}
