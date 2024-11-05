using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class recomendationProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecomendationComplete",
                columns: table => new
                {
                    recomendationCompleteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recomendationId = table.Column<int>(type: "int", nullable: false),
                    dateCompleted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecomendationComplete", x => x.recomendationCompleteId);
                    table.ForeignKey(
                        name: "FK_RecomendationComplete_patients_PatientuserId",
                        column: x => x.PatientuserId,
                        principalTable: "patients",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecomendationComplete_PatientuserId",
                table: "RecomendationComplete",
                column: "PatientuserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecomendationComplete");
        }
    }
}
