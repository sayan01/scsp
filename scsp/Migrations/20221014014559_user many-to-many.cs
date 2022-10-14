using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scsp.Migrations
{
    public partial class usermanytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.CreateTable(
                name: "Relation",
                columns: table => new
                {
                    FromId = table.Column<string>(type: "TEXT", nullable: false),
                    ToId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relation", x => new { x.FromId, x.ToId });
                    table.ForeignKey(
                        name: "FK_Relation_User_FromId",
                        column: x => x.FromId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relation_User_ToId",
                        column: x => x.ToId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relation_ToId",
                table: "Relation",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relation");

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FollowedByUserID = table.Column<string>(type: "TEXT", nullable: false),
                    FollowsUserID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FollowedByUserID, x.FollowsUserID });
                    table.ForeignKey(
                        name: "FK_UserUser_User_FollowedByUserID",
                        column: x => x.FollowedByUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_User_FollowsUserID",
                        column: x => x.FollowsUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_FollowsUserID",
                table: "UserUser",
                column: "FollowsUserID");
        }
    }
}
