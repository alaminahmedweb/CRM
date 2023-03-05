using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterbuildingdetails1502 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingDetails_Customers_CusotmerId",
                table: "BuildingDetails");

            migrationBuilder.RenameColumn(
                name: "CusotmerId",
                table: "BuildingDetails",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BuildingDetails_CusotmerId",
                table: "BuildingDetails",
                newName: "IX_BuildingDetails_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingDetails_Customers_CustomerId",
                table: "BuildingDetails",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingDetails_Customers_CustomerId",
                table: "BuildingDetails");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "BuildingDetails",
                newName: "CusotmerId");

            migrationBuilder.RenameIndex(
                name: "IX_BuildingDetails_CustomerId",
                table: "BuildingDetails",
                newName: "IX_BuildingDetails_CusotmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingDetails_Customers_CusotmerId",
                table: "BuildingDetails",
                column: "CusotmerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
