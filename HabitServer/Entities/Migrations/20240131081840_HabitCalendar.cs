using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitServer.Migrations
{
    /// <inheritdoc />
    public partial class HabitCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calendar",
                table: "Habits",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calendar",
                table: "Habits");
        }
    }
}
