using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labs.Excel.Loader.Database.Migrations
{
    public partial class Update_c_Aduana_Normalize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_c_Aduana",
                table: "c_Aduana");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "c_Aduana");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "c_Aduana");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "c_Aduana",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "c_Aduana",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "c_Aduana",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "c_Aduana",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_c_Aduana",
                table: "c_Aduana",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_c_Aduana",
                table: "c_Aduana");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "c_Aduana");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "c_Aduana");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "c_Aduana",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "c_Aduana",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "c_Aduana",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "c_Aduana",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_c_Aduana",
                table: "c_Aduana",
                column: "Clave");
        }
    }
}
