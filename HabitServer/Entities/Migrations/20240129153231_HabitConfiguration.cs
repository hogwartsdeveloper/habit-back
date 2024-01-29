using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitServer.Migrations
{
    /// <inheritdoc />
    public partial class HabitConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habit_Users_UserId",
                table: "Habit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Habit",
                table: "Habit");

            migrationBuilder.DropIndex(
                name: "IX_Habit_UserId",
                table: "Habit");

            migrationBuilder.RenameTable(
                name: "Habit",
                newName: "Habits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Habits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Habits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Habits",
                table: "Habits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_Users_Id",
                table: "Habits",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_Users_Id",
                table: "Habits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Habits",
                table: "Habits");

            migrationBuilder.RenameTable(
                name: "Habits",
                newName: "Habit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Habit",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Habit",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Habit",
                table: "Habit",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Habit_UserId",
                table: "Habit",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habit_Users_UserId",
                table: "Habit",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
