using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitServer.Migrations
{
    /// <inheritdoc />
    public partial class HabitIsOverdue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOverdue",
                table: "Habit",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOverdue",
                table: "Habit");
        }
    }
}
