using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class UserContents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XIdentityUserContent",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ContentId = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XIdentityUserContent", x => new { x.UserId, x.ContentId });
                    table.ForeignKey(
                        name: "FK_XIdentityUserContent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_XIdentityUserContent_ContentObjects_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentObjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_XIdentityUserContent_ContentId",
                table: "XIdentityUserContent",
                column: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XIdentityUserContent");
        }
    }
}
