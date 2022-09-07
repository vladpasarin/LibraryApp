using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class NewChangesv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_GoalId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
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
                name: "FK_Goals_GoalTypes_GoalId",
                table: "Goals",
                column: "GoalId",
                principalTable: "GoalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
