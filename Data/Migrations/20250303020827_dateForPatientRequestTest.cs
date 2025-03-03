using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class dateForPatientRequestTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecomendationComplete_patients_PatientuserId",
                table: "RecomendationComplete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecomendationComplete",
                table: "RecomendationComplete");

            migrationBuilder.RenameTable(
                name: "RecomendationComplete",
                newName: "recomendationComplete");

            migrationBuilder.RenameIndex(
                name: "IX_RecomendationComplete_PatientuserId",
                table: "recomendationComplete",
                newName: "IX_recomendationComplete_PatientuserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "patientRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_recomendationComplete",
                table: "recomendationComplete",
                column: "recomendationCompleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_recomendationComplete_patients_PatientuserId",
                table: "recomendationComplete",
                column: "PatientuserId",
                principalTable: "patients",
                principalColumn: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recomendationComplete_patients_PatientuserId",
                table: "recomendationComplete");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recomendationComplete",
                table: "recomendationComplete");

            migrationBuilder.DropColumn(
                name: "date",
                table: "patientRequest");

            migrationBuilder.RenameTable(
                name: "recomendationComplete",
                newName: "RecomendationComplete");

            migrationBuilder.RenameIndex(
                name: "IX_recomendationComplete_PatientuserId",
                table: "RecomendationComplete",
                newName: "IX_RecomendationComplete_PatientuserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecomendationComplete",
                table: "RecomendationComplete",
                column: "recomendationCompleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecomendationComplete_patients_PatientuserId",
                table: "RecomendationComplete",
                column: "PatientuserId",
                principalTable: "patients",
                principalColumn: "userId");
        }
    }
}
