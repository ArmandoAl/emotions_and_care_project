using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class subtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Especialidad",
                table: "Especialistas",
                newName: "Enfoque");

            migrationBuilder.AddColumn<int>(
                name: "SubTitulo",
                table: "Recomendaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTitulo",
                table: "Recomendaciones");

            migrationBuilder.RenameColumn(
                name: "Enfoque",
                table: "Especialistas",
                newName: "Especialidad");
        }
    }
}
