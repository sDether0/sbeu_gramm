using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SBEU.Gramm.DataLayer.Migrations
{
    public partial class testrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Posts_PostId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Contents");

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "Contents",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                columns: new[] { "ContentId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Posts_PostId",
                table: "Contents",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Posts_PostId",
                table: "Contents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contents",
                table: "Contents");

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "Contents",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Contents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contents",
                table: "Contents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentId",
                table: "Contents",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Posts_PostId",
                table: "Contents",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
