using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class ChangesV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals",
                column: "GoalTypeId",
                principalTable: "GoalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals",
                column: "GoalTypeId",
                principalTable: "GoalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
