using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class UpdateRatingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Rating",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Rating");
        }
    }
}
