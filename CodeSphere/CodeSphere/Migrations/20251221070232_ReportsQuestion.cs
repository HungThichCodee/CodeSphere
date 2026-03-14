using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeSphere.Migrations
{
    /// <inheritdoc />
    public partial class ReportsQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Votes",
                table: "Questions",
                newName: "ReportsCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportsCount",
                table: "Questions",
                newName: "Votes");
        }
    }
}
