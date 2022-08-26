using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TercihSihirbazi.Data.Migrations
{
    public partial class manyToManyRelationCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserDetailObject",
                columns: table => new
                {
                    AppUserFavoritesId = table.Column<int>(type: "integer", nullable: false),
                    FavoritedAppUsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserDetailObject", x => new { x.AppUserFavoritesId, x.FavoritedAppUsersId });
                    table.ForeignKey(
                        name: "FK_AppUserDetailObject_AppUsers_FavoritedAppUsersId",
                        column: x => x.FavoritedAppUsersId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserDetailObject_ExcelData_AppUserFavoritesId",
                        column: x => x.AppUserFavoritesId,
                        principalTable: "ExcelData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserDetailObject_FavoritedAppUsersId",
                table: "AppUserDetailObject",
                column: "FavoritedAppUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserDetailObject");
        }
    }
}
