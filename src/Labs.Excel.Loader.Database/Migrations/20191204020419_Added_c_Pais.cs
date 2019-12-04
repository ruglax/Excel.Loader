using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Added_c_Pais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_Pais",
                columns: table => new
                {
                    Clave = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    FormatoCodigoPostal = table.Column<string>(nullable: true),
                    FormatoRegistroIdentidad = table.Column<string>(nullable: true),
                    ValidacionRegistroIdentidad = table.Column<string>(nullable: true),
                    Agrupaciones = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_Pais", x => x.Clave);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_Pais");
        }
    }
}
