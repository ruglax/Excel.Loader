using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Added_c_Moneda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_Moneda",
                columns: table => new
                {
                    Clave = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Decimales = table.Column<short>(nullable: false),
                    PorcentajeVariacion = table.Column<short>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Moneda", x => x.Clave);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_Moneda");
        }
    }
}
