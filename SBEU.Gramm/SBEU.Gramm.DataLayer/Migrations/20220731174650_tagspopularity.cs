using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class tagspopularity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_ContentObjects_SoundId",
                table: "Stories");

            migrationBuilder.AddColumn<decimal>(
                name: "Popularity",
                table: "Tags",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "SoundId",
                table: "Stories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_ContentObjects_SoundId",
                table: "Stories",
                column: "SoundId",
                principalTable: "ContentObjects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_ContentObjects_SoundId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "SoundId",
                table: "Stories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_ContentObjects_SoundId",
                table: "Stories",
                column: "SoundId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
