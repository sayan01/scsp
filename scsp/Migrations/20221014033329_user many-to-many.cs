using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class usermanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_FollowedByUserID",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_FollowsUserID",
                table: "UserUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "FollowsUserID",
                table: "UserUser",
                newName: "FollowerId");

            migrationBuilder.RenameColumn(
                name: "FollowedByUserID",
                table: "UserUser",
                newName: "FolloweeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_FollowsUserID",
                table: "UserUser",
                newName: "IX_UserUser_FollowerId");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "UserUser",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_FolloweeId",
                table: "UserUser",
                column: "FolloweeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_FolloweeId",
                table: "UserUser",
                column: "FolloweeId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_FollowerId",
                table: "UserUser",
                column: "FollowerId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_FolloweeId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_User_FollowerId",
                table: "UserUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser");

            migrationBuilder.DropIndex(
                name: "IX_UserUser_FolloweeId",
                table: "UserUser");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "UserUser",
                newName: "FollowsUserID");

            migrationBuilder.RenameColumn(
                name: "FolloweeId",
                table: "UserUser",
                newName: "FollowedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_FollowerId",
                table: "UserUser",
                newName: "IX_UserUser_FollowsUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUser",
                table: "UserUser",
                columns: new[] { "FollowedByUserID", "FollowsUserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_FollowedByUserID",
                table: "UserUser",
                column: "FollowedByUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_User_FollowsUserID",
                table: "UserUser",
                column: "FollowsUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
