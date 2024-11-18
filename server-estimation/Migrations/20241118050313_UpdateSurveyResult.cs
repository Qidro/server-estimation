using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_estimation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSurveyResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "SurveyResults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SurveyResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "SurveyResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SurveyResults",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
