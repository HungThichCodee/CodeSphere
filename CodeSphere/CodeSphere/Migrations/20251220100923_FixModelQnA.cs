using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Migrations
{
    /// <inheritdoc />
    public partial class FixModelQnA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_ApplicationUsers_ApplicationUserId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_ApplicationUsers_ApplicationUserId",
                table: "Answers",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_ApplicationUsers_ApplicationUserId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_ApplicationUsers_ApplicationUserId",
                table: "Answers",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
