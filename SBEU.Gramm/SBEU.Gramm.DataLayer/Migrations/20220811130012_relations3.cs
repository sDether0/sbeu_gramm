using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class relations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Posts_NPostId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stories_NStoryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stories_NStoryId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AspNetUsers_AuthorId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NPostId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NStoryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NStoryId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NPostId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NStoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NStoryId1",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Stories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "NPostXIdentityUser",
                columns: table => new
                {
                    PTaggedInId = table.Column<string>(type: "text", nullable: false),
                    TaggedUsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPostXIdentityUser", x => new { x.PTaggedInId, x.TaggedUsersId });
                    table.ForeignKey(
                        name: "FK_NPostXIdentityUser_AspNetUsers_TaggedUsersId",
                        column: x => x.TaggedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPostXIdentityUser_Posts_PTaggedInId",
                        column: x => x.PTaggedInId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NStoryXIdentityUser",
                columns: table => new
                {
                    STaggedInId = table.Column<string>(type: "text", nullable: false),
                    TaggedUsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NStoryXIdentityUser", x => new { x.STaggedInId, x.TaggedUsersId });
                    table.ForeignKey(
                        name: "FK_NStoryXIdentityUser_AspNetUsers_TaggedUsersId",
                        column: x => x.TaggedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NStoryXIdentityUser_Stories_STaggedInId",
                        column: x => x.STaggedInId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NStoryXIdentityUser1",
                columns: table => new
                {
                    WatchedId = table.Column<string>(type: "text", nullable: false),
                    WatchedUsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NStoryXIdentityUser1", x => new { x.WatchedId, x.WatchedUsersId });
                    table.ForeignKey(
                        name: "FK_NStoryXIdentityUser1_AspNetUsers_WatchedUsersId",
                        column: x => x.WatchedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NStoryXIdentityUser1_Stories_WatchedId",
                        column: x => x.WatchedId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NPostXIdentityUser_TaggedUsersId",
                table: "NPostXIdentityUser",
                column: "TaggedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_NStoryXIdentityUser_TaggedUsersId",
                table: "NStoryXIdentityUser",
                column: "TaggedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_NStoryXIdentityUser1_WatchedUsersId",
                table: "NStoryXIdentityUser1",
                column: "WatchedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AspNetUsers_AuthorId",
                table: "Stories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_AspNetUsers_AuthorId",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "NPostXIdentityUser");

            migrationBuilder.DropTable(
                name: "NStoryXIdentityUser");

            migrationBuilder.DropTable(
                name: "NStoryXIdentityUser1");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Stories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "NPostId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NStoryId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NStoryId1",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NPostId",
                table: "AspNetUsers",
                column: "NPostId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NStoryId",
                table: "AspNetUsers",
                column: "NStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NStoryId1",
                table: "AspNetUsers",
                column: "NStoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Posts_NPostId",
                table: "AspNetUsers",
                column: "NPostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stories_NStoryId",
                table: "AspNetUsers",
                column: "NStoryId",
                principalTable: "Stories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stories_NStoryId1",
                table: "AspNetUsers",
                column: "NStoryId1",
                principalTable: "Stories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_AspNetUsers_AuthorId",
                table: "Stories",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
