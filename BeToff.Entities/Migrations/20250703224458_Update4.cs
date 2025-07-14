using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeToff.Entities.Migrations
{
    /// <inheritdoc />
    public partial class Update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_IdAuthor",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "IdAuthor",
                table: "Photos",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_IdAuthor",
                table: "Photos",
                newName: "IX_Photos_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_AuthorId",
                table: "Photos",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_AuthorId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Photos",
                newName: "IdAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_AuthorId",
                table: "Photos",
                newName: "IX_Photos_IdAuthor");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_IdAuthor",
                table: "Photos",
                column: "IdAuthor",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
