using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class addauthoridforeignkeytopost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_AuthorUserID",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "AuthorUserID",
                table: "Post",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_AuthorUserID",
                table: "Post",
                newName: "IX_Post_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_AuthorId",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Post",
                newName: "AuthorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Post_AuthorId",
                table: "Post",
                newName: "IX_Post_AuthorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_AuthorUserID",
                table: "Post",
                column: "AuthorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
