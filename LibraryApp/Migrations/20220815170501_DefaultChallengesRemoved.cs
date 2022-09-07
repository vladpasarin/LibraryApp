using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class DefaultChallengesRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Challenges",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Challenges",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Challenges",
                keyColumn: "Id",
                keyValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "Id", "Completed", "DateCompleted", "DateStarted", "Description", "Name", "Started", "Threshold" },
                values: new object[,]
                {
                    { 1, false, null, null, "Borrow your first book!", "Newbie Reader", false, 0 },
                    { 2, false, null, null, "Bookmark 3 or more books!", "Bookmark Enthusiast", false, 0 },
                    { 3, false, null, null, "Rate 3 or more books!", "Opinionated Reader", false, 0 }
                });
        }
    }
}
