using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class sound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoundId",
                table: "Stories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_SoundId",
                table: "Stories",
                column: "SoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_ContentObjects_SoundId",
                table: "Stories",
                column: "SoundId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_ContentObjects_SoundId",
                table: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Stories_SoundId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "SoundId",
                table: "Stories");
        }
    }
}
