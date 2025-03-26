using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_diaries_diaryId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_emotions_emotionId",
                table: "Note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "notes");

            migrationBuilder.RenameIndex(
                name: "IX_Note_emotionId",
                table: "notes",
                newName: "IX_notes_emotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_diaryId",
                table: "notes",
                newName: "IX_notes_diaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notes",
                table: "notes",
                column: "noteId");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_diaries_diaryId",
                table: "notes",
                column: "diaryId",
                principalTable: "diaries",
                principalColumn: "diaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_emotions_emotionId",
                table: "notes",
                column: "emotionId",
                principalTable: "emotions",
                principalColumn: "emotionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_diaries_diaryId",
                table: "notes");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_emotions_emotionId",
                table: "notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notes",
                table: "notes");

            migrationBuilder.RenameTable(
                name: "notes",
                newName: "Note");

            migrationBuilder.RenameIndex(
                name: "IX_notes_emotionId",
                table: "Note",
                newName: "IX_Note_emotionId");

            migrationBuilder.RenameIndex(
                name: "IX_notes_diaryId",
                table: "Note",
                newName: "IX_Note_diaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "noteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_diaries_diaryId",
                table: "Note",
                column: "diaryId",
                principalTable: "diaries",
                principalColumn: "diaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_emotions_emotionId",
                table: "Note",
                column: "emotionId",
                principalTable: "emotions",
                principalColumn: "emotionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
