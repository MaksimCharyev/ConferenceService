using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConferenceService.Migrations
{
    /// <inheritdoc />
    public partial class Activities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "activities",
                columns: new[] { "Id", "activity", "description" },
                values: new object[,]
                {
                    { 1, 1, "Доклад, 35 - 45 минут" },
                    { 2, 3, "Дискуссия / круглый стол, 40-50 минут" },
                    { 3, 2, "Мастеркласс, 1-2 часа" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "activities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
