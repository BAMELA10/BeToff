using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeToff.Entities.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateJoined = table.Column<DateOnly>(type: "date", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Famillies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOfFamilly = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreation = table.Column<DateOnly>(type: "date", nullable: false),
                    IdCreator = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdHead = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Famillies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Famillies_Users_IdCreator",
                        column: x => x.IdCreator,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Famillies_Users_IdHead",
                        column: x => x.IdHead,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreation = table.Column<DateOnly>(type: "date", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamillyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Famillies_FamillyId",
                        column: x => x.FamillyId,
                        principalTable: "Famillies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitations_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamillyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfregistation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registration_Famillies_FamillyId",
                        column: x => x.FamillyId,
                        principalTable: "Famillies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    DateCreation = table.Column<DateOnly>(type: "date", nullable: false),
                    IdAuthor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPhoto = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Photos_IdPhoto",
                        column: x => x.IdPhoto,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_IdAuthor",
                        column: x => x.IdAuthor,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdAuthor",
                table: "Comments",
                column: "IdAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IdPhoto",
                table: "Comments",
                column: "IdPhoto");

            migrationBuilder.CreateIndex(
                name: "IX_Famillies_IdCreator",
                table: "Famillies",
                column: "IdCreator");

            migrationBuilder.CreateIndex(
                name: "IX_Famillies_IdHead",
                table: "Famillies",
                column: "IdHead");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_FamillyId",
                table: "Invitations",
                column: "FamillyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ReceiverId",
                table: "Invitations",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_SenderId",
                table: "Invitations",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AuthorId",
                table: "Photos",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_FamillyId",
                table: "Registration",
                column: "FamillyId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_UserId",
                table: "Registration",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Famillies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
