using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class follow4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Follower",
                table: "Follows");

            migrationBuilder.RenameColumn(
                name: "Following",
                table: "Follows",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Follows",
                newName: "Following");

            migrationBuilder.AddColumn<string>(
                name: "Follower",
                table: "Follows",
                type: "text",
                nullable: true);
        }
    }
}
