using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class IsLiked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "Stories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "Posts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "ContentObjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "Commentaries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "ContentObjects");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "Commentaries");
        }
    }
}
