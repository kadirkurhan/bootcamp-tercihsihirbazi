using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TercihSihirbazi.Data.Migrations
{
    public partial class TabanAndTavanColumnsAddedToExcelData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TabanList",
                table: "ExcelData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TavanList",
                table: "ExcelData",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TabanList",
                table: "ExcelData");

            migrationBuilder.DropColumn(
                name: "TavanList",
                table: "ExcelData");
        }
    }
}
