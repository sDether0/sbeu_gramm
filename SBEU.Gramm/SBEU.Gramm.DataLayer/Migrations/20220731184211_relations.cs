using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.CreateTable(
                name: "NContentObjectNPost",
                columns: table => new
                {
                    ContentsId = table.Column<string>(type: "text", nullable: false),
                    PostsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NContentObjectNPost", x => new { x.ContentsId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_NContentObjectNPost_ContentObjects_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "ContentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NContentObjectNPost_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPostTags",
                columns: table => new
                {
                    PostsId = table.Column<string>(type: "text", nullable: false),
                    TagsId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPostTags", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_NPostTags_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPostTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NContentObjectNPost_PostsId",
                table: "NContentObjectNPost",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_NPostTags_TagsId",
                table: "NPostTags",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NContentObjectNPost");

            migrationBuilder.DropTable(
                name: "NPostTags");

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ContentsId = table.Column<string>(type: "text", nullable: false),
                    PostsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_ContentObjects_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "ContentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contents_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PostsId = table.Column<string>(type: "text", nullable: false),
                    TagsId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentsId",
                table: "Contents",
                column: "ContentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_PostsId",
                table: "Contents",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostsId",
                table: "PostTags",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagsId",
                table: "PostTags",
                column: "TagsId");
        }
    }
}
