using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class title_views : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NStoryXIdentityUser1_Stories_WatchedId",
                table: "NStoryXIdentityUser1");

            migrationBuilder.RenameColumn(
                name: "WatchedId",
                table: "NStoryXIdentityUser1",
                newName: "WatchedStoriesId");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "NPostXIdentityUser1",
                columns: table => new
                {
                    WatchedPostsId = table.Column<string>(type: "text", nullable: false),
                    WatchedUsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPostXIdentityUser1", x => new { x.WatchedPostsId, x.WatchedUsersId });
                    table.ForeignKey(
                        name: "FK_NPostXIdentityUser1_AspNetUsers_WatchedUsersId",
                        column: x => x.WatchedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPostXIdentityUser1_Posts_WatchedPostsId",
                        column: x => x.WatchedPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NPostXIdentityUser1_WatchedUsersId",
                table: "NPostXIdentityUser1",
                column: "WatchedUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_NStoryXIdentityUser1_Stories_WatchedStoriesId",
                table: "NStoryXIdentityUser1",
                column: "WatchedStoriesId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NStoryXIdentityUser1_Stories_WatchedStoriesId",
                table: "NStoryXIdentityUser1");

            migrationBuilder.DropTable(
                name: "NPostXIdentityUser1");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "WatchedStoriesId",
                table: "NStoryXIdentityUser1",
                newName: "WatchedId");

            migrationBuilder.AddForeignKey(
                name: "FK_NStoryXIdentityUser1_Stories_WatchedId",
                table: "NStoryXIdentityUser1",
                column: "WatchedId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
