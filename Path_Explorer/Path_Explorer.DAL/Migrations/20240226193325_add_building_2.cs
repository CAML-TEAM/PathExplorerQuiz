using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Path_Explorer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_building_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                schema: "Core",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buildings",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Coordinate = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_BuildingId",
                schema: "Core",
                table: "Questions",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_SoftDeleted",
                schema: "Core",
                table: "Buildings",
                column: "SoftDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                schema: "Core",
                table: "Questions",
                column: "BuildingId",
                principalSchema: "Core",
                principalTable: "Buildings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Buildings_BuildingId",
                schema: "Core",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Buildings",
                schema: "Core");

            migrationBuilder.DropIndex(
                name: "IX_Questions_BuildingId",
                schema: "Core",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "Core",
                table: "Questions");
        }
    }
}
