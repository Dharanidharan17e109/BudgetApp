using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budget_App.Migrations
{
    /// <inheritdoc />
    public partial class descroiptionadddedformesthiri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MesthiriAmts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MesthiriAmts");
        }
    }
}
