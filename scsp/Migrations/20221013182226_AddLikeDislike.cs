using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class AddLikeDislike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DislikeComment",
                columns: table => new
                {
                    DislikeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorUserID = table.Column<string>(type: "TEXT", nullable: false),
                    CommentID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikeComment", x => x.DislikeID);
                    table.ForeignKey(
                        name: "FK_DislikeComment_Comment_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DislikeComment_User_AuthorUserID",
                        column: x => x.AuthorUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DislikePost",
                columns: table => new
                {
                    DislikeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorUserID = table.Column<string>(type: "TEXT", nullable: false),
                    PostID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikePost", x => x.DislikeID);
                    table.ForeignKey(
                        name: "FK_DislikePost_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DislikePost_User_AuthorUserID",
                        column: x => x.AuthorUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeComment",
                columns: table => new
                {
                    LikeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorUserID = table.Column<string>(type: "TEXT", nullable: false),
                    CommentID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeComment", x => x.LikeID);
                    table.ForeignKey(
                        name: "FK_LikeComment_Comment_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeComment_User_AuthorUserID",
                        column: x => x.AuthorUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikePost",
                columns: table => new
                {
                    LikeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorUserID = table.Column<string>(type: "TEXT", nullable: false),
                    PostID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikePost", x => x.LikeID);
                    table.ForeignKey(
                        name: "FK_LikePost_Post_PostID",
                        column: x => x.PostID,
                        principalTable: "Post",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikePost_User_AuthorUserID",
                        column: x => x.AuthorUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DislikeComment_AuthorUserID",
                table: "DislikeComment",
                column: "AuthorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DislikeComment_CommentID",
                table: "DislikeComment",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_DislikePost_AuthorUserID",
                table: "DislikePost",
                column: "AuthorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DislikePost_PostID",
                table: "DislikePost",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComment_AuthorUserID",
                table: "LikeComment",
                column: "AuthorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComment_CommentID",
                table: "LikeComment",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_LikePost_AuthorUserID",
                table: "LikePost",
                column: "AuthorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LikePost_PostID",
                table: "LikePost",
                column: "PostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DislikeComment");

            migrationBuilder.DropTable(
                name: "DislikePost");

            migrationBuilder.DropTable(
                name: "LikeComment");

            migrationBuilder.DropTable(
                name: "LikePost");
        }
    }
}
