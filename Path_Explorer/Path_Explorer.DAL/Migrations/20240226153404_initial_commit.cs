using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Path_Explorer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial_commit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IsAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Core",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizDetails",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    QuizId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    OptionId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizDetails_QuestionOptions_OptionId",
                        column: x => x.OptionId,
                        principalSchema: "Core",
                        principalTable: "QuestionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizDetails_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Core",
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizDetails_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalSchema: "Core",
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                schema: "Core",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_SoftDeleted",
                schema: "Core",
                table: "QuestionOptions",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SoftDeleted",
                schema: "Core",
                table: "Questions",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_QuizDetails_OptionId",
                schema: "Core",
                table: "QuizDetails",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizDetails_QuestionId",
                schema: "Core",
                table: "QuizDetails",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizDetails_QuizId",
                schema: "Core",
                table: "QuizDetails",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizDetails_SoftDeleted",
                schema: "Core",
                table: "QuizDetails",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_SoftDeleted",
                schema: "Core",
                table: "Quizzes",
                column: "SoftDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizDetails",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "QuestionOptions",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Quizzes",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "Core");
        }
    }
}
