using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class diaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Diary_diaryId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_Diary_diaryId",
                table: "patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diary",
                table: "Diary");

            migrationBuilder.RenameTable(
                name: "Diary",
                newName: "diaries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_diaries",
                table: "diaries",
                column: "diaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_diaries_diaryId",
                table: "Note",
                column: "diaryId",
                principalTable: "diaries",
                principalColumn: "diaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_diaries_diaryId",
                table: "patients",
                column: "diaryId",
                principalTable: "diaries",
                principalColumn: "diaryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_diaries_diaryId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_patients_diaries_diaryId",
                table: "patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_diaries",
                table: "diaries");

            migrationBuilder.RenameTable(
                name: "diaries",
                newName: "Diary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diary",
                table: "Diary",
                column: "diaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Diary_diaryId",
                table: "Note",
                column: "diaryId",
                principalTable: "Diary",
                principalColumn: "diaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_patients_Diary_diaryId",
                table: "patients",
                column: "diaryId",
                principalTable: "Diary",
                principalColumn: "diaryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
