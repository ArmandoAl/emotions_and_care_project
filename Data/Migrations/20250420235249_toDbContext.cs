using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class toDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "achievementCollections",
                columns: table => new
                {
                    achievementCollectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_achievementCollections", x => x.achievementCollectionId);
                });

            migrationBuilder.CreateTable(
                name: "userAchievements",
                columns: table => new
                {
                    userAchievementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    achievementId = table.Column<int>(type: "int", nullable: false),
                    progress = table.Column<int>(type: "int", nullable: false),
                    dateEarned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    achievementCollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAchievements", x => x.userAchievementId);
                    table.ForeignKey(
                        name: "FK_userAchievements_achievementCollections_achievementCollectionId",
                        column: x => x.achievementCollectionId,
                        principalTable: "achievementCollections",
                        principalColumn: "achievementCollectionId");
                    table.ForeignKey(
                        name: "FK_userAchievements_achievements_achievementId",
                        column: x => x.achievementId,
                        principalTable: "achievements",
                        principalColumn: "achievementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userAchievements_achievementCollectionId",
                table: "userAchievements",
                column: "achievementCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_userAchievements_achievementId",
                table: "userAchievements",
                column: "achievementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userAchievements");

            migrationBuilder.DropTable(
                name: "achievementCollections");
        }
    }
}
