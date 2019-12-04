using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Added_c_TasaOCuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "c_TasaOCuota",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TasaOCuotaType = table.Column<int>(nullable: false),
                    ValorMinimo = table.Column<string>(nullable: true),
                    ValorMinimoNumeric = table.Column<float>(nullable: true),
                    ValorMaximo = table.Column<string>(nullable: true),
                    ValorMaximoNumeric = table.Column<float>(nullable: true),
                    Impuesto = table.Column<string>(nullable: true),
                    Factor = table.Column<string>(nullable: true),
                    Traslado = table.Column<int>(nullable: false),
                    Retencion = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_c_TasaOCuota", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "c_TasaOCuota");
        }
    }
}
