using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Update_c_FormaPago__Added_c_Impuesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "c_FormaPago",
                newName: "Nombre");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "c_FormaPago",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicio",
                table: "c_FormaPago",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "c_FormaPago");

            migrationBuilder.DropColumn(
                name: "FechaInicio",
                table: "c_FormaPago");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "c_FormaPago",
                newName: "Descripcion");
        }
    }
}
