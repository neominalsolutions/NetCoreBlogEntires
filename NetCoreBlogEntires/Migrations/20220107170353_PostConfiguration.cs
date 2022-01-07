using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreBlogEntires.Migrations
{
    public partial class PostConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Posts_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostsId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Makale");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Makale",
                newName: "Makale Başlıgı");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CategoryId",
                table: "Makale",
                newName: "IX_Makale_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ShortContent",
                table: "Makale",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Makale Başlıgı",
                table: "Makale",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Makale",
                table: "Makale",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Makale_Makale Başlıgı",
                table: "Makale",
                column: "Makale Başlıgı",
                unique: true,
                filter: "[Makale Başlıgı] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Makale_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Makale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Makale_Categories_CategoryId",
                table: "Makale",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Makale_PostsId",
                table: "PostTag",
                column: "PostsId",
                principalTable: "Makale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Makale_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Makale_Categories_CategoryId",
                table: "Makale");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Makale_PostsId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Makale",
                table: "Makale");

            migrationBuilder.DropIndex(
                name: "IX_Makale_Makale Başlıgı",
                table: "Makale");

            migrationBuilder.RenameTable(
                name: "Makale",
                newName: "Posts");

            migrationBuilder.RenameColumn(
                name: "Makale Başlıgı",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Makale_CategoryId",
                table: "Posts",
                newName: "IX_Posts_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ShortContent",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Posts_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostsId",
                table: "PostTag",
                column: "PostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
