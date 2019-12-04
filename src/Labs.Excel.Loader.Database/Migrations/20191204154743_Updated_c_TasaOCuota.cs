using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Updated_c_TasaOCuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorMaximoNumeric",
                table: "c_TasaOCuota");

            migrationBuilder.DropColumn(
                name: "ValorMinimoNumeric",
                table: "c_TasaOCuota");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ValorMaximoNumeric",
                table: "c_TasaOCuota",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ValorMinimoNumeric",
                table: "c_TasaOCuota",
                nullable: true);
        }
    }
}
