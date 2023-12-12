using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamedThemeToSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Theme_ThemeId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.RenameColumn(
                name: "ThemeId",
                table: "Books",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_ThemeId",
                table: "Books",
                newName: "IX_Books_SubjectId");

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Subjects_SubjectId",
                table: "Books",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Subjects_SubjectId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Books",
                newName: "ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_SubjectId",
                table: "Books",
                newName: "IX_Books_ThemeId");

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Theme_ThemeId",
                table: "Books",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
