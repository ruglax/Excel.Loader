using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Updated_c_Moneda_ProcentajeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "PorcentajeVariacion",
                table: "c_Moneda",
                nullable: true,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "PorcentajeVariacion",
                table: "c_Moneda",
                nullable: false,
                oldClrType: typeof(short),
                oldNullable: true);
        }
    }
}
