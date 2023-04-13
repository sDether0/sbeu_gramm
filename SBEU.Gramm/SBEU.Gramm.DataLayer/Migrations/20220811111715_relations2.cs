using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class relations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "TagUsers");

            migrationBuilder.DropTable(
                name: "WatchedUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts");

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
                table: "Posts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "TagUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    NPostId = table.Column<string>(type: "text", nullable: true),
                    NStoryId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TagUsers_Posts_NPostId",
                        column: x => x.NPostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TagUsers_Stories_NStoryId",
                        column: x => x.NStoryId,
                        principalTable: "Stories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WatchedUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    NStoryId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchedUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchedUsers_Stories_NStoryId",
                        column: x => x.NStoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagUsers_NPostId",
                table: "TagUsers",
                column: "NPostId");

            migrationBuilder.CreateIndex(
                name: "IX_TagUsers_NStoryId",
                table: "TagUsers",
                column: "NStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TagUsers_UserId",
                table: "TagUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedUsers_NStoryId",
                table: "WatchedUsers",
                column: "NStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedUsers_UserId",
                table: "WatchedUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
