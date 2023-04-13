using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class follow2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentObjects_ContentId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Posts_PostId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowersId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId1",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_FollowingId1",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WatchedUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserConfirmations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TagUsers");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "FollowersId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "FollowerId",
                table: "Follows");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostTags",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FollowingId1",
                table: "Follows",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Contents",
                newName: "PostsId");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "Contents",
                newName: "ContentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_PostId",
                table: "Contents",
                newName: "IX_Contents_PostsId");

            migrationBuilder.AlterColumn<string>(
                name: "FollowingId",
                table: "Follows",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Contents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostsId",
                table: "PostTags",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentsId",
                table: "Contents",
                column: "ContentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentObjects_ContentsId",
                table: "Contents",
                column: "ContentsId",
                principalTable: "ContentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Posts_PostsId",
                table: "Contents",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows",
                column: "FollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentObjects_ContentsId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Posts_PostsId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_PostsId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentsId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PostTags",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Follows",
                newName: "FollowingId1");

            migrationBuilder.RenameColumn(
                name: "PostsId",
                table: "Contents",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "ContentsId",
                table: "Contents",
                newName: "ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Contents_PostsId",
                table: "Contents",
                newName: "IX_Contents_PostId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WatchedUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserConfirmations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TagUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TagId",
                table: "PostTags",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "FollowingId",
                table: "Follows",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "FollowersId",
                table: "Follows",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FollowerId",
                table: "Follows",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                columns: new[] { "PostsId", "TagsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                columns: new[] { "FollowersId", "FollowingId1" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                columns: new[] { "ContentId", "PostId" });

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowingId1",
                table: "Follows",
                column: "FollowingId1");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowersId",
                table: "Follows",
                column: "FollowersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId",
                table: "Follows",
                column: "FollowingId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_FollowingId1",
                table: "Follows",
                column: "FollowingId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
