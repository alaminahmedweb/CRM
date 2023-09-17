using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingByIdtobooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingById",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingById",
                table: "Bookings",
                column: "BookingById");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Employees_BookingById",
                table: "Bookings",
                column: "BookingById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Employees_BookingById",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingById",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingById",
                table: "Bookings");
        }
    }
}
