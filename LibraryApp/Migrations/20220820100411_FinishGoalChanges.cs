using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class FinishGoalChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_GoalType_GoalTypeId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Users_UserId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Users_UserId",
                table: "Quote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quote",
                table: "Quote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalType",
                table: "GoalType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.RenameTable(
                name: "Quote",
                newName: "Quotes");

            migrationBuilder.RenameTable(
                name: "GoalType",
                newName: "GoalTypes");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.RenameIndex(
                name: "IX_Quote_UserId",
                table: "Quotes",
                newName: "IX_Quotes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_UserId",
                table: "Goals",
                newName: "IX_Goals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_GoalTypeId",
                table: "Goals",
                newName: "IX_Goals_GoalTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalTypes",
                table: "GoalTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "Id");

            migrationBuilder.InsertData(
                table: "GoalTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Borrow a set number of books", "Newbie reader" },
                    { 2, "Bookmark a set number of books", "Bookmark Enjoyer" },
                    { 3, "Rate a set number of books!", "Opinionated Reader" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals",
                column: "GoalTypeId",
                principalTable: "GoalTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Users_UserId",
                table: "Quotes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_GoalTypeId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Users_UserId",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quotes",
                table: "Quotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalTypes",
                table: "GoalTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.DeleteData(
                table: "GoalTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GoalTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GoalTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Quotes",
                newName: "Quote");

            migrationBuilder.RenameTable(
                name: "GoalTypes",
                newName: "GoalType");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_UserId",
                table: "Quote",
                newName: "IX_Quote_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_UserId",
                table: "Goal",
                newName: "IX_Goal_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_GoalTypeId",
                table: "Goal",
                newName: "IX_Goal_GoalTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quote",
                table: "Quote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalType",
                table: "GoalType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_GoalType_GoalTypeId",
                table: "Goal",
                column: "GoalTypeId",
                principalTable: "GoalType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Users_UserId",
                table: "Goal",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Users_UserId",
                table: "Quote",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
