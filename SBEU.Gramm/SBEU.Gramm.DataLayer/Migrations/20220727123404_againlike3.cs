using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class againlike3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaries_Likes");

            migrationBuilder.DropTable(
                name: "ContentObjects_Likes");

            migrationBuilder.DropTable(
                name: "Posts_Likes");

            migrationBuilder.DropTable(
                name: "Stories_Likes");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    LikeObjectId = table.Column<string>(type: "text", nullable: false),
                    NCommentaryId = table.Column<string>(type: "text", nullable: true),
                    NPostId = table.Column<string>(type: "text", nullable: true),
                    NStoryId = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Commentaries_NCommentaryId",
                        column: x => x.NCommentaryId,
                        principalTable: "Commentaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_ContentObjects_LikeObjectId",
                        column: x => x.LikeObjectId,
                        principalTable: "ContentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Posts_NPostId",
                        column: x => x.NPostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Stories_NStoryId",
                        column: x => x.NStoryId,
                        principalTable: "Stories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AuthorId",
                table: "Likes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikeObjectId",
                table: "Likes",
                column: "LikeObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NCommentaryId",
                table: "Likes",
                column: "NCommentaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NPostId",
                table: "Likes",
                column: "NPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NStoryId",
                table: "Likes",
                column: "NStoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.CreateTable(
                name: "Commentaries_Likes",
                columns: table => new
                {
                    LikeObjectId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaries_Likes", x => new { x.LikeObjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_Commentaries_Likes_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commentaries_Likes_Commentaries_LikeObjectId",
                        column: x => x.LikeObjectId,
                        principalTable: "Commentaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentObjects_Likes",
                columns: table => new
                {
                    LikeObjectId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentObjects_Likes", x => new { x.LikeObjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_ContentObjects_Likes_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentObjects_Likes_ContentObjects_LikeObjectId",
                        column: x => x.LikeObjectId,
                        principalTable: "ContentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts_Likes",
                columns: table => new
                {
                    LikeObjectId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts_Likes", x => new { x.LikeObjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_Posts_Likes_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Likes_Posts_LikeObjectId",
                        column: x => x.LikeObjectId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stories_Likes",
                columns: table => new
                {
                    LikeObjectId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories_Likes", x => new { x.LikeObjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_Stories_Likes_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stories_Likes_Stories_LikeObjectId",
                        column: x => x.LikeObjectId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_Likes_AuthorId",
                table: "Commentaries_Likes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentObjects_Likes_AuthorId",
                table: "ContentObjects_Likes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Likes_AuthorId",
                table: "Posts_Likes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_Likes_AuthorId",
                table: "Stories_Likes",
                column: "AuthorId");
        }
    }
}
