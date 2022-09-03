using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TercihSihirbazi.Data.Migrations
{
    public partial class ExcelDatabaseYerlesenColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Yerlesen",
                table: "ExcelData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserFavorites_DetailObjectId",
                table: "AppUserFavorites",
                column: "DetailObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFavorites_AppUsers_AppUserId",
                table: "AppUserFavorites",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFavorites_ExcelData_DetailObjectId",
                table: "AppUserFavorites",
                column: "DetailObjectId",
                principalTable: "ExcelData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFavorites_AppUsers_AppUserId",
                table: "AppUserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFavorites_ExcelData_DetailObjectId",
                table: "AppUserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_AppUserFavorites_DetailObjectId",
                table: "AppUserFavorites");

            migrationBuilder.DropColumn(
                name: "Yerlesen",
                table: "ExcelData");
        }
    }
}
