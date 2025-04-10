using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Dummy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "badgeCollectionId",
                table: "badges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BadgeCollection",
                columns: table => new
                {
                    badgeCollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadgeCollection", x => x.badgeCollectionId);
                });

            migrationBuilder.CreateTable(
                name: "dummyUsers",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Test = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    badgeCollectionId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    relationalToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    termsAndConditionsId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dummyUsers", x => x.userId);
                    table.ForeignKey(
                        name: "FK_dummyUsers_BadgeCollection_badgeCollectionId",
                        column: x => x.badgeCollectionId,
                        principalTable: "BadgeCollection",
                        principalColumn: "badgeCollectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dummyUsers_terms_termsAndConditionsId",
                        column: x => x.termsAndConditionsId,
                        principalTable: "terms",
                        principalColumn: "termsAndConditionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_badges_badgeCollectionId",
                table: "badges",
                column: "badgeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_dummyUsers_badgeCollectionId",
                table: "dummyUsers",
                column: "badgeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_dummyUsers_termsAndConditionsId",
                table: "dummyUsers",
                column: "termsAndConditionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_badges_BadgeCollection_badgeCollectionId",
                table: "badges",
                column: "badgeCollectionId",
                principalTable: "BadgeCollection",
                principalColumn: "badgeCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_badges_BadgeCollection_badgeCollectionId",
                table: "badges");

            migrationBuilder.DropTable(
                name: "dummyUsers");

            migrationBuilder.DropTable(
                name: "BadgeCollection");

            migrationBuilder.DropIndex(
                name: "IX_badges_badgeCollectionId",
                table: "badges");

            migrationBuilder.DropColumn(
                name: "badgeCollectionId",
                table: "badges");
        }
    }
}
