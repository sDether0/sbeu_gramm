using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class againlike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Commentaries_NCommentaryId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_NPostId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Stories_NStoryId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_NCommentaryId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_NPostId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_NStoryId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "NCommentaryId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "NPostId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "NStoryId",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Commentaries_LikeObjectId",
                table: "Likes",
                column: "LikeObjectId",
                principalTable: "Commentaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_LikeObjectId",
                table: "Likes",
                column: "LikeObjectId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Stories_LikeObjectId",
                table: "Likes",
                column: "LikeObjectId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Commentaries_LikeObjectId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_LikeObjectId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Stories_LikeObjectId",
                table: "Likes");

            migrationBuilder.AddColumn<string>(
                name: "NCommentaryId",
                table: "Likes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NPostId",
                table: "Likes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NStoryId",
                table: "Likes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NCommentaryId",
                table: "Likes",
                column: "NCommentaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NPostId",
                table: "Likes",
                column: "NPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_NStoryId",
                table: "Likes",
                column: "NStoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Commentaries_NCommentaryId",
                table: "Likes",
                column: "NCommentaryId",
                principalTable: "Commentaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_NPostId",
                table: "Likes",
                column: "NPostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Stories_NStoryId",
                table: "Likes",
                column: "NStoryId",
                principalTable: "Stories",
                principalColumn: "Id");
        }
    }
}
