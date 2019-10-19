using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_Aduana",
                columns: table => new
                {
                    Clave = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Aduana", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "c_ClaveProdServ",
                columns: table => new
                {
                    Clave = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    IncluirIVA = table.Column<int>(nullable: false),
                    IncluirIEPS = table.Column<int>(nullable: false),
                    EstimuloFranjaFronteriza = table.Column<short>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_ClaveProdServ", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "c_ClaveUnidad",
                columns: table => new
                {
                    Clave = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_ClaveUnidad", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "c_CodigoPostal",
                columns: table => new
                {
                    Clave = table.Column<string>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Localidad = table.Column<string>(nullable: true),
                    EstimuloFranjaFronteriza = table.Column<short>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_CodigoPostal", x => x.Clave);
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
        }
    }
}
