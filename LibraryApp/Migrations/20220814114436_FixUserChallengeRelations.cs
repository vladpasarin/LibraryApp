using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class FixUserChallengeRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_Users_UserId",
                table: "Challenges");

            migrationBuilder.DropIndex(
                name: "IX_Challenges_UserId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Challenges");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Challenges",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_UserId",
                table: "Challenges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_Users_UserId",
                table: "Challenges",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
