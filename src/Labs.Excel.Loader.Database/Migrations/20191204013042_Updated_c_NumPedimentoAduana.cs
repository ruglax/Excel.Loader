using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Updated_c_NumPedimentoAduana : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_c_NumPedimentoAduana",
                table: "c_NumPedimentoAduana");

            migrationBuilder.AlterColumn<string>(
                name: "Ejercicio",
                table: "c_NumPedimentoAduana",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Patente",
                table: "c_NumPedimentoAduana",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Aduana",
                table: "c_NumPedimentoAduana",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "c_NumPedimentoAduana",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_c_NumPedimentoAduana",
                table: "c_NumPedimentoAduana",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_c_NumPedimentoAduana",
                table: "c_NumPedimentoAduana");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "c_NumPedimentoAduana");

            migrationBuilder.AlterColumn<string>(
                name: "Patente",
                table: "c_NumPedimentoAduana",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ejercicio",
                table: "c_NumPedimentoAduana",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Aduana",
                table: "c_NumPedimentoAduana",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_c_NumPedimentoAduana",
                table: "c_NumPedimentoAduana",
                columns: new[] { "Aduana", "Patente", "Ejercicio" });
        }
    }
}
