using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatePostUserAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActions_BaseUserActions_BaseUserActionId",
                table: "UserActions");

            migrationBuilder.DropIndex(
                name: "IX_UserActions_BaseUserActionId",
                table: "UserActions");

            migrationBuilder.AlterColumn<string>(
                name: "BaseUserActionId",
                table: "UserActions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseUserActions_UserActions_UserActionId",
                table: "BaseUserActions",
                column: "UserActionId",
                principalTable: "UserActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseUserActions_UserActions_UserActionId",
                table: "BaseUserActions");

            migrationBuilder.AlterColumn<string>(
                name: "BaseUserActionId",
                table: "UserActions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_BaseUserActionId",
                table: "UserActions",
                column: "BaseUserActionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActions_BaseUserActions_BaseUserActionId",
                table: "UserActions",
                column: "BaseUserActionId",
                principalTable: "BaseUserActions",
                principalColumn: "UserActionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
