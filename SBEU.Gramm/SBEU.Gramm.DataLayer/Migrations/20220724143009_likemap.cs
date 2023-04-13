using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class likemap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_ContentObjects_NContentObjectId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_NContentObjectId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "NContentObjectId",
                table: "Likes");

            migrationBuilder.AddColumn<string>(
                name: "LikeObjectId",
                table: "Likes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikeObjectId",
                table: "Likes",
                column: "LikeObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_ContentObjects_LikeObjectId",
                table: "Likes",
                column: "LikeObjectId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_ContentObjects_LikeObjectId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_LikeObjectId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "LikeObjectId",
                table: "Likes");

            migrationBuilder.AddColumn<string>(
                name: "NContentObjectId",
                table: "Likes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NContentObjectId",
                table: "Likes",
                column: "NContentObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_ContentObjects_NContentObjectId",
                table: "Likes",
                column: "NContentObjectId",
                principalTable: "ContentObjects",
                principalColumn: "Id");
        }
    }
}
