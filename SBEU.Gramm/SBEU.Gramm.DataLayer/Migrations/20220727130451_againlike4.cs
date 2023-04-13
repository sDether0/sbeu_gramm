using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class againlike4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Commentaries_NCommentaryId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_ContentObjects_LikeObjectId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_NPostId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Stories_NStoryId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_LikeObjectId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "LikeObjectId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "NStoryId",
                table: "Likes",
                newName: "StoryId");

            migrationBuilder.RenameColumn(
                name: "NPostId",
                table: "Likes",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "NCommentaryId",
                table: "Likes",
                newName: "ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_NStoryId",
                table: "Likes",
                newName: "IX_Likes_StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_NPostId",
                table: "Likes",
                newName: "IX_Likes_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_NCommentaryId",
                table: "Likes",
                newName: "IX_Likes_ContentId");

            migrationBuilder.AddColumn<string>(
                name: "CommentaryId",
                table: "Likes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CommentaryId",
                table: "Likes",
                column: "CommentaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Commentaries_CommentaryId",
                table: "Likes",
                column: "CommentaryId",
                principalTable: "Commentaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_ContentObjects_ContentId",
                table: "Likes",
                column: "ContentId",
                principalTable: "ContentObjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Stories_StoryId",
                table: "Likes",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Commentaries_CommentaryId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_ContentObjects_ContentId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Posts_PostId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Stories_StoryId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CommentaryId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CommentaryId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Likes",
                newName: "NStoryId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Likes",
                newName: "NPostId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "Likes",
                newName: "NCommentaryId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_StoryId",
                table: "Likes",
                newName: "IX_Likes_NStoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                newName: "IX_Likes_NPostId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ContentId",
                table: "Likes",
                newName: "IX_Likes_NCommentaryId");

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
                name: "FK_Likes_Commentaries_NCommentaryId",
                table: "Likes",
                column: "NCommentaryId",
                principalTable: "Commentaries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_ContentObjects_LikeObjectId",
                table: "Likes",
                column: "LikeObjectId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
