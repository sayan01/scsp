using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class Addforeignkeypostidtocomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostID",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "PostID",
                table: "Comment",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostID",
                table: "Comment",
                newName: "IX_Comment_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Comment",
                newName: "PostID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                newName: "IX_Comment_PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostID",
                table: "Comment",
                column: "PostID",
                principalTable: "Post",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
