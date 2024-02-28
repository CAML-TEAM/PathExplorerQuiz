using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Path_Explorer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_question_tbl_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "Core",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "Core",
                table: "Questions");
        }
    }
}
