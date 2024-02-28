using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Path_Explorer.DAL.Migrations.PathExplorerMssqlDb
{
    /// <inheritdoc />
    public partial class add_building : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coordinate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_BuildingId",
                table: "Questions",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_SoftDeleted",
                table: "Buildings",
                column: "SoftDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                table: "Questions",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Questions_BuildingId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Questions");
        }
    }
}
