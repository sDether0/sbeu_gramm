using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentObjects_Posts_NPostId",
                table: "ContentObjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_ContentObjects_NPostId",
                table: "ContentObjects");

            migrationBuilder.DropColumn(
                name: "NPostId",
                table: "ContentObjects");

            migrationBuilder.AddColumn<decimal>(
                name: "Id",
                table: "Tags",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NContent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<string>(type: "text", nullable: true),
                    ContentId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NContent_ContentObjects_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NContent_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NContent_ContentId",
                table: "NContent",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_NContent_PostId",
                table: "NContent",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "NPostId",
                table: "ContentObjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Tag");

            migrationBuilder.CreateIndex(
                name: "IX_ContentObjects_NPostId",
                table: "ContentObjects",
                column: "NPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentObjects_Posts_NPostId",
                table: "ContentObjects",
                column: "NPostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
