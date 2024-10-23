using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_estimation.Migrations
{
    /// <inheritdoc />
    public partial class RenameJobTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JogTitle",
                table: "Users",
                newName: "JobTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobTitle",
                table: "Users",
                newName: "JogTitle");
        }
    }
}
