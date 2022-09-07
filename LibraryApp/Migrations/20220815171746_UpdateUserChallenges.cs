using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Migrations
{
    public partial class UpdateUserChallenges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "DateStarted",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "Started",
                table: "Challenges");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "UserChallenges",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "UserChallenges",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStarted",
                table: "UserChallenges",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Started",
                table: "UserChallenges",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Threshold",
                table: "UserChallenges",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "DateStarted",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "Started",
                table: "UserChallenges");

            migrationBuilder.DropColumn(
                name: "Threshold",
                table: "UserChallenges");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Challenges",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "Challenges",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStarted",
                table: "Challenges",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Started",
                table: "Challenges",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
