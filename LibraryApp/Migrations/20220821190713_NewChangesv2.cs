using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class NewChangesv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "GoalTypeId",
                table: "Goals",
                newName: "GoalId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_GoalTypeId",
                table: "Goals",
                newName: "IX_Goals_GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_GoalTypes_UserId",
                table: "Goals",
                column: "UserId",
                principalTable: "GoalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_GoalId",
                table: "Goals",
                column: "GoalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_UserId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_GoalId",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "GoalId",
                table: "Goals",
                newName: "GoalTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_GoalId",
                table: "Goals",
                newName: "IX_Goals_GoalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals",
                column: "GoalTypeId",
                principalTable: "GoalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
