using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class cartAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartAnswer_carts_cartId",
                table: "CartAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartAnswer",
                table: "CartAnswer");

            migrationBuilder.RenameTable(
                name: "CartAnswer",
                newName: "cartAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_CartAnswer_cartId",
                table: "cartAnswers",
                newName: "IX_cartAnswers_cartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartAnswers",
                table: "cartAnswers",
                column: "cartAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartAnswers_carts_cartId",
                table: "cartAnswers",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "cartId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartAnswers_carts_cartId",
                table: "cartAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartAnswers",
                table: "cartAnswers");

            migrationBuilder.RenameTable(
                name: "cartAnswers",
                newName: "CartAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_cartAnswers_cartId",
                table: "CartAnswer",
                newName: "IX_CartAnswer_cartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartAnswer",
                table: "CartAnswer",
                column: "cartAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartAnswer_carts_cartId",
                table: "CartAnswer",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "cartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
