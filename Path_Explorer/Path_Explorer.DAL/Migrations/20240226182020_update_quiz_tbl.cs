﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Path_Explorer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_quiz_tbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "Core",
                table: "Quizzes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "QuizStatus",
                schema: "Core",
                table: "Quizzes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizStatus",
                schema: "Core",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "Core",
                table: "Quizzes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}