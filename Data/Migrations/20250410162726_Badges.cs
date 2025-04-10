using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Badges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dayGiven",
                table: "UserSticker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "latestUpdate",
                table: "UserSticker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "timesEarned",
                table: "UserSticker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "badges",
                columns: table => new
                {
                    badgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    progressMap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_badges", x => x.badgeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "badges");

            migrationBuilder.DropColumn(
                name: "dayGiven",
                table: "UserSticker");

            migrationBuilder.DropColumn(
                name: "latestUpdate",
                table: "UserSticker");

            migrationBuilder.DropColumn(
                name: "timesEarned",
                table: "UserSticker");
        }
    }
}
