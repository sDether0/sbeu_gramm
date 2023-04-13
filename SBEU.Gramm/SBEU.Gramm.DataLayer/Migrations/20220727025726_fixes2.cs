using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class fixes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NContent_ContentObjects_ContentId",
                table: "NContent");

            migrationBuilder.DropForeignKey(
                name: "FK_NContent_Posts_PostId",
                table: "NContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NContent",
                table: "NContent");

            migrationBuilder.RenameTable(
                name: "NContent",
                newName: "Contents");

            migrationBuilder.RenameIndex(
                name: "IX_NContent_PostId",
                table: "Contents",
                newName: "IX_Contents_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_NContent_ContentId",
                table: "Contents",
                newName: "IX_Contents_ContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentObjects_ContentId",
                table: "Contents",
                column: "ContentId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Posts_PostId",
                table: "Contents",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentObjects_ContentId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Posts_PostId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.RenameTable(
                name: "Contents",
                newName: "NContent");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_PostId",
                table: "NContent",
                newName: "IX_NContent_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_ContentId",
                table: "NContent",
                newName: "IX_NContent_ContentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NContent",
                table: "NContent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NContent_ContentObjects_ContentId",
                table: "NContent",
                column: "ContentId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NContent_Posts_PostId",
                table: "NContent",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
