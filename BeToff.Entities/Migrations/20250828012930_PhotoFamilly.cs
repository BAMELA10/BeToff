using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeToff.Entities.Migrations
{
    /// <inheritdoc />
    public partial class PhotoFamilly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Famillies_FamillyId",
                table: "Invitations");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Photos",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "FamillyId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdCreator",
                table: "Famillies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_FamillyId",
                table: "Photos",
                column: "FamillyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Famillies_FamillyId",
                table: "Invitations",
                column: "FamillyId",
                principalTable: "Famillies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Famillies_FamillyId",
                table: "Photos",
                column: "FamillyId",
                principalTable: "Famillies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Famillies_FamillyId",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Famillies_FamillyId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_FamillyId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "FamillyId",
                table: "Photos");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdCreator",
                table: "Famillies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Famillies_FamillyId",
                table: "Invitations",
                column: "FamillyId",
                principalTable: "Famillies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
