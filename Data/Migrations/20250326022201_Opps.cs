using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Opps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bukayo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bukayo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "opps",
                columns: table => new
                {
                    BuserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bukayoSakaDiaryId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_opps", x => x.BuserId);
                    table.ForeignKey(
                        name: "FK_opps_Bukayo_bukayoSakaDiaryId",
                        column: x => x.bukayoSakaDiaryId,
                        principalTable: "Bukayo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SakaNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BukayoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SakaNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SakaNotes_Bukayo_BukayoId",
                        column: x => x.BukayoId,
                        principalTable: "Bukayo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_opps_bukayoSakaDiaryId",
                table: "opps",
                column: "bukayoSakaDiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SakaNotes_BukayoId",
                table: "SakaNotes",
                column: "BukayoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "opps");

            migrationBuilder.DropTable(
                name: "SakaNotes");

            migrationBuilder.DropTable(
                name: "Bukayo");
        }
    }
}
