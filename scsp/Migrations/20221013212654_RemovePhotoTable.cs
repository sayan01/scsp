using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class RemovePhotoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Photo_PhotoID",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Photo_PhotoID",
                table: "User");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_User_PhotoID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Post_PhotoID",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "PhotoID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhotoID",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Post",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Post");

            migrationBuilder.AddColumn<int>(
                name: "PhotoID",
                table: "User",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoID",
                table: "Post",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    PhotoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhotoBLOB = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.PhotoID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PhotoID",
                table: "User",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_Post_PhotoID",
                table: "Post",
                column: "PhotoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Photo_PhotoID",
                table: "Post",
                column: "PhotoID",
                principalTable: "Photo",
                principalColumn: "PhotoID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Photo_PhotoID",
                table: "User",
                column: "PhotoID",
                principalTable: "Photo",
                principalColumn: "PhotoID");
        }
    }
}
