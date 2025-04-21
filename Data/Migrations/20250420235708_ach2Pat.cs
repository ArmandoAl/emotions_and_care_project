using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ach2Pat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "achievementCollectionId",
                table: "patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_patients_achievementCollectionId",
                table: "patients",
                column: "achievementCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_achievementCollections_achievementCollectionId",
                table: "patients",
                column: "achievementCollectionId",
                principalTable: "achievementCollections",
                principalColumn: "achievementCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_achievementCollections_achievementCollectionId",
                table: "patients");

            migrationBuilder.DropIndex(
                name: "IX_patients_achievementCollectionId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "achievementCollectionId",
                table: "patients");
        }
    }
}
