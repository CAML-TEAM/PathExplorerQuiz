using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Path_Explorer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_building_2_NN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                schema: "Core",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                schema: "Core",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                schema: "Core",
                table: "Questions",
                column: "BuildingId",
                principalSchema: "Core",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                schema: "Core",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                schema: "Core",
                table: "Questions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                schema: "Core",
                table: "Questions",
                column: "BuildingId",
                principalSchema: "Core",
                principalTable: "Buildings",
                principalColumn: "Id");
        }
    }
}
