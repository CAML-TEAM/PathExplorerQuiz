using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Path_Explorer.DAL.Migrations.PathExplorerMssqlDb
{
    /// <inheritdoc />
    public partial class add_building_NN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                table: "Questions",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "BuildingId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                table: "Questions",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }
    }
}
