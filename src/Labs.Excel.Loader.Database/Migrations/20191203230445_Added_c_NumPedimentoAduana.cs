using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Added_c_NumPedimentoAduana : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_NumPedimentoAduana",
                columns: table => new
                {
                    Aduana = table.Column<string>(nullable: false),
                    Patente = table.Column<string>(nullable: false),
                    Ejercicio = table.Column<string>(nullable: false),
                    Cantidad = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_NumPedimentoAduana", x => new { x.Aduana, x.Patente, x.Ejercicio });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_NumPedimentoAduana");
        }
    }
}
