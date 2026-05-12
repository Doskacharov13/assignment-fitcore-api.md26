using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "WorkoutClasses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "WorkoutClasses",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "WorkoutClasses",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "WorkoutClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
