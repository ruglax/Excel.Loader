using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class UpdatedColoniaCatalogDefinition1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "c_Colonia",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "c_Colonia",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5);
        }
    }
}
