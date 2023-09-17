using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class complainregisteradded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceFeedbacks_Bookings_bookingId",
                table: "ServiceFeedbacks");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "ServiceFeedbacks",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceFeedbacks_bookingId",
                table: "ServiceFeedbacks",
                newName: "IX_ServiceFeedbacks_BookingId");

            migrationBuilder.CreateTable(
                name: "Complains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplainDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complains_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complains_BookingId",
                table: "Complains",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceFeedbacks_Bookings_BookingId",
                table: "ServiceFeedbacks",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceFeedbacks_Bookings_BookingId",
                table: "ServiceFeedbacks");

            migrationBuilder.DropTable(
                name: "Complains");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "ServiceFeedbacks",
                newName: "bookingId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceFeedbacks_BookingId",
                table: "ServiceFeedbacks",
                newName: "IX_ServiceFeedbacks_bookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceFeedbacks_Bookings_bookingId",
                table: "ServiceFeedbacks",
                column: "bookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
