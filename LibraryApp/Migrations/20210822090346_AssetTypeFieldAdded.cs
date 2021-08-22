using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApp.Migrations
{
    public partial class AssetTypeFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Assets_AssetId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Users_UserId",
                table: "Bookmarks");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Assets",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Assets_AssetId",
                table: "Bookmarks",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Users_UserId",
                table: "Bookmarks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Assets_AssetId",
                table: "Bookmarks");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Users_UserId",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Assets");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Assets_AssetId",
                table: "Bookmarks",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Users_UserId",
                table: "Bookmarks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
