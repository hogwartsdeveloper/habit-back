using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitServer.Migrations
{
    /// <inheritdoc />
    public partial class HabitFixConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_Users_Id",
                table: "Habits");

            migrationBuilder.CreateIndex(
                name: "IX_Habits_UserId",
                table: "Habits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_Users_UserId",
                table: "Habits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_Users_UserId",
                table: "Habits");

            migrationBuilder.DropIndex(
                name: "IX_Habits_UserId",
                table: "Habits");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_Users_Id",
                table: "Habits",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
