using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_estimation.Migrations
{
    /// <inheritdoc />
    public partial class AddDivisionAndJobTtitleUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Divisions",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JogTitle",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Divisions",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JogTitle",
                table: "Users");
        }
    }
}
