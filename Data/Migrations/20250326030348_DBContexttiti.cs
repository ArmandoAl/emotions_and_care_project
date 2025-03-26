using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class DBContexttiti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_opps_Bukayo_bukayoSakaDiaryId",
                table: "opps");

            migrationBuilder.DropForeignKey(
                name: "FK_SakaNotes_Bukayo_BukayoId",
                table: "SakaNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SakaNotes",
                table: "SakaNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bukayo",
                table: "Bukayo");

            migrationBuilder.RenameTable(
                name: "SakaNotes",
                newName: "sakaNotes");

            migrationBuilder.RenameTable(
                name: "Bukayo",
                newName: "bukayos");

            migrationBuilder.RenameIndex(
                name: "IX_SakaNotes_BukayoId",
                table: "sakaNotes",
                newName: "IX_sakaNotes_BukayoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sakaNotes",
                table: "sakaNotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bukayos",
                table: "bukayos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_opps_bukayos_bukayoSakaDiaryId",
                table: "opps",
                column: "bukayoSakaDiaryId",
                principalTable: "bukayos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sakaNotes_bukayos_BukayoId",
                table: "sakaNotes",
                column: "BukayoId",
                principalTable: "bukayos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_opps_bukayos_bukayoSakaDiaryId",
                table: "opps");

            migrationBuilder.DropForeignKey(
                name: "FK_sakaNotes_bukayos_BukayoId",
                table: "sakaNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sakaNotes",
                table: "sakaNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bukayos",
                table: "bukayos");

            migrationBuilder.RenameTable(
                name: "sakaNotes",
                newName: "SakaNotes");

            migrationBuilder.RenameTable(
                name: "bukayos",
                newName: "Bukayo");

            migrationBuilder.RenameIndex(
                name: "IX_sakaNotes_BukayoId",
                table: "SakaNotes",
                newName: "IX_SakaNotes_BukayoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SakaNotes",
                table: "SakaNotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bukayo",
                table: "Bukayo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_opps_Bukayo_bukayoSakaDiaryId",
                table: "opps",
                column: "bukayoSakaDiaryId",
                principalTable: "Bukayo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SakaNotes_Bukayo_BukayoId",
                table: "SakaNotes",
                column: "BukayoId",
                principalTable: "Bukayo",
                principalColumn: "Id");
        }
    }
}
