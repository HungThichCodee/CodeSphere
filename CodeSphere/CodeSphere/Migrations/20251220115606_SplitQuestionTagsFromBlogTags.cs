using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Migrations
{
    /// <inheritdoc />
    public partial class SplitQuestionTagsFromBlogTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTags_TagId",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "QuestionTags");

            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "QuestionTags",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagName",
                table: "QuestionTags");

            migrationBuilder.AddColumn<string>(
                name: "TagId",
                table: "QuestionTags",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTags_TagId",
                table: "QuestionTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
