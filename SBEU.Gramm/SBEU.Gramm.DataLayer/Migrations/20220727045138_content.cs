using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Stories",
                newName: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ContentId",
                table: "Stories",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_ContentObjects_ContentId",
                table: "Stories",
                column: "ContentId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_ContentObjects_ContentId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_ContentId",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "Stories",
                newName: "Content");
        }
    }
}
