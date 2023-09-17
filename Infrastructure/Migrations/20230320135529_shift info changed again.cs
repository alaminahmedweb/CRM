using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shiftinfochangedagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShiftInfos",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ShiftInfos",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.InsertData(
                table: "ShiftInfos",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Morning" },
                    { 2, "Evening" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShiftInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShiftInfos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "ShiftInfos",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 100, "Morning" },
                    { 200, "Evening" }
                });
        }
    }
}
