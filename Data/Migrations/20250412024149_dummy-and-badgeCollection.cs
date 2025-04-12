using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class dummyandbadgeCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "badgeCollections",
                columns: table => new
                {
                    badgeCollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_badgeCollections", x => x.badgeCollectionId);
                });

            migrationBuilder.CreateTable(
                name: "dummyUsers",
                columns: table => new
                {
                    BuserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    favoritePlayer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favoriteTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    favoriteStadium = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dummyUsers", x => x.BuserId);
                    table.ForeignKey(
                        name: "FK_dummyUsers_badgeCollections_badgeCollectionId",
                        column: x => x.badgeCollectionId,
                        principalTable: "badgeCollections",
                        principalColumn: "badgeCollectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userBadges",
                columns: table => new
                {
                    userBadgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    badgeId = table.Column<int>(type: "int", nullable: false),
                    progress = table.Column<int>(type: "int", nullable: false),
                    dateEarned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    badgeCollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userBadges", x => x.userBadgeId);
                    table.ForeignKey(
                        name: "FK_userBadges_badgeCollections_badgeCollectionId",
                        column: x => x.badgeCollectionId,
                        principalTable: "badgeCollections",
                        principalColumn: "badgeCollectionId");
                    table.ForeignKey(
                        name: "FK_userBadges_badges_badgeId",
                        column: x => x.badgeId,
                        principalTable: "badges",
                        principalColumn: "badgeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dummyUsers_badgeCollectionId",
                table: "dummyUsers",
                column: "badgeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_userBadges_badgeCollectionId",
                table: "userBadges",
                column: "badgeCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_userBadges_badgeId",
                table: "userBadges",
                column: "badgeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dummyUsers");

            migrationBuilder.DropTable(
                name: "userBadges");

            migrationBuilder.DropTable(
                name: "badgeCollections");
        }
    }
}
