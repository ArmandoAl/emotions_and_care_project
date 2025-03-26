using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class patientRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patientRequest_patients_patientuserId",
                table: "patientRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_patientRequest_specialists_SpecialistuserId",
                table: "patientRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_patientRequest",
                table: "patientRequest");

            migrationBuilder.RenameTable(
                name: "patientRequest",
                newName: "PatientRequest");

            migrationBuilder.RenameIndex(
                name: "IX_patientRequest_SpecialistuserId",
                table: "PatientRequest",
                newName: "IX_PatientRequest_SpecialistuserId");

            migrationBuilder.RenameIndex(
                name: "IX_patientRequest_patientuserId",
                table: "PatientRequest",
                newName: "IX_PatientRequest_patientuserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientRequest",
                table: "PatientRequest",
                column: "patientRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRequest_patients_patientuserId",
                table: "PatientRequest",
                column: "patientuserId",
                principalTable: "patients",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRequest_specialists_SpecialistuserId",
                table: "PatientRequest",
                column: "SpecialistuserId",
                principalTable: "specialists",
                principalColumn: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRequest_patients_patientuserId",
                table: "PatientRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientRequest_specialists_SpecialistuserId",
                table: "PatientRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientRequest",
                table: "PatientRequest");

            migrationBuilder.RenameTable(
                name: "PatientRequest",
                newName: "patientRequest");

            migrationBuilder.RenameIndex(
                name: "IX_PatientRequest_SpecialistuserId",
                table: "patientRequest",
                newName: "IX_patientRequest_SpecialistuserId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientRequest_patientuserId",
                table: "patientRequest",
                newName: "IX_patientRequest_patientuserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_patientRequest",
                table: "patientRequest",
                column: "patientRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_patientRequest_patients_patientuserId",
                table: "patientRequest",
                column: "patientuserId",
                principalTable: "patients",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patientRequest_specialists_SpecialistuserId",
                table: "patientRequest",
                column: "SpecialistuserId",
                principalTable: "specialists",
                principalColumn: "userId");
        }
    }
}
