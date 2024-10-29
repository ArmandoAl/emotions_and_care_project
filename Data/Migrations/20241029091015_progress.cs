using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class progress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "progressId",
                table: "patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    progressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stage = table.Column<int>(type: "int", nullable: false),
                    lastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    begginDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => x.progressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patients_progressId",
                table: "patients",
                column: "progressId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_Progress_progressId",
                table: "patients",
                column: "progressId",
                principalTable: "Progress",
                principalColumn: "progressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patients_Progress_progressId",
                table: "patients");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropIndex(
                name: "IX_patients_progressId",
                table: "patients");

            migrationBuilder.DropColumn(
                name: "progressId",
                table: "patients");
        }
    }
}
