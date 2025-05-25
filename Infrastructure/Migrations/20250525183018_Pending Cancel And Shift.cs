using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PendingCancelAndShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PendingAgreeAmount",
                table: "Followups",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PendingBookingById",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PendingBookingNote",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PendingEntryDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PendingShiftDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PendingShiftId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PendingTeamId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PendingAgreeAmount",
                table: "Followups");

            migrationBuilder.DropColumn(
                name: "PendingBookingById",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PendingBookingNote",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PendingEntryDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PendingShiftDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PendingShiftId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PendingTeamId",
                table: "Bookings");
        }
    }
}
