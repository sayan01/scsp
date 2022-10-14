using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class Addforeignkeyauthoridtolikeanddisliketablesandcommenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_AuthorUserID",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_DislikeComment_User_AuthorUserID",
                table: "DislikeComment");

            migrationBuilder.DropForeignKey(
                name: "FK_DislikePost_User_AuthorUserID",
                table: "DislikePost");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeComment_User_AuthorUserID",
                table: "LikeComment");

            migrationBuilder.DropForeignKey(
                name: "FK_LikePost_User_AuthorUserID",
                table: "LikePost");

            migrationBuilder.RenameColumn(
                name: "AuthorUserID",
                table: "LikePost",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_LikePost_AuthorUserID",
                table: "LikePost",
                newName: "IX_LikePost_AuthorId");

            migrationBuilder.RenameColumn(
                name: "AuthorUserID",
                table: "LikeComment",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_LikeComment_AuthorUserID",
                table: "LikeComment",
                newName: "IX_LikeComment_AuthorId");

            migrationBuilder.RenameColumn(
                name: "AuthorUserID",
                table: "DislikePost",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_DislikePost_AuthorUserID",
                table: "DislikePost",
                newName: "IX_DislikePost_AuthorId");

            migrationBuilder.RenameColumn(
                name: "AuthorUserID",
                table: "DislikeComment",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_DislikeComment_AuthorUserID",
                table: "DislikeComment",
                newName: "IX_DislikeComment_AuthorId");

            migrationBuilder.RenameColumn(
                name: "AuthorUserID",
                table: "Comment",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorUserID",
                table: "Comment",
                newName: "IX_Comment_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DislikeComment_User_AuthorId",
                table: "DislikeComment",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DislikePost_User_AuthorId",
                table: "DislikePost",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeComment_User_AuthorId",
                table: "LikeComment",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikePost_User_AuthorId",
                table: "LikePost",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_DislikeComment_User_AuthorId",
                table: "DislikeComment");

            migrationBuilder.DropForeignKey(
                name: "FK_DislikePost_User_AuthorId",
                table: "DislikePost");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeComment_User_AuthorId",
                table: "LikeComment");

            migrationBuilder.DropForeignKey(
                name: "FK_LikePost_User_AuthorId",
                table: "LikePost");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "LikePost",
                newName: "AuthorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_LikePost_AuthorId",
                table: "LikePost",
                newName: "IX_LikePost_AuthorUserID");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "LikeComment",
                newName: "AuthorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_LikeComment_AuthorId",
                table: "LikeComment",
                newName: "IX_LikeComment_AuthorUserID");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "DislikePost",
                newName: "AuthorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_DislikePost_AuthorId",
                table: "DislikePost",
                newName: "IX_DislikePost_AuthorUserID");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "DislikeComment",
                newName: "AuthorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_DislikeComment_AuthorId",
                table: "DislikeComment",
                newName: "IX_DislikeComment_AuthorUserID");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Comment",
                newName: "AuthorUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                newName: "IX_Comment_AuthorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_AuthorUserID",
                table: "Comment",
                column: "AuthorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DislikeComment_User_AuthorUserID",
                table: "DislikeComment",
                column: "AuthorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DislikePost_User_AuthorUserID",
                table: "DislikePost",
                column: "AuthorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeComment_User_AuthorUserID",
                table: "LikeComment",
                column: "AuthorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikePost_User_AuthorUserID",
                table: "LikePost",
                column: "AuthorUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
