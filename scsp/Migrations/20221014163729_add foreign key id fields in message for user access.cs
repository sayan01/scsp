using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class addforeignkeyidfieldsinmessageforuseraccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_FromUserID",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_ToUserID",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ToUserID",
                table: "Message",
                newName: "ToId");

            migrationBuilder.RenameColumn(
                name: "FromUserID",
                table: "Message",
                newName: "FromId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ToUserID",
                table: "Message",
                newName: "IX_Message_ToId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_FromUserID",
                table: "Message",
                newName: "IX_Message_FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_FromId",
                table: "Message",
                column: "FromId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_ToId",
                table: "Message",
                column: "ToId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_FromId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_ToId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "Message",
                newName: "ToUserID");

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "Message",
                newName: "FromUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ToId",
                table: "Message",
                newName: "IX_Message_ToUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Message_FromId",
                table: "Message",
                newName: "IX_Message_FromUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_FromUserID",
                table: "Message",
                column: "FromUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_ToUserID",
                table: "Message",
                column: "ToUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
