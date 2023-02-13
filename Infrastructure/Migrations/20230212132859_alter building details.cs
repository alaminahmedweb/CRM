using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterbuildingdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "BuildingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BuildingDetails_BrandId",
                table: "BuildingDetails",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingDetails_Brands_BrandId",
                table: "BuildingDetails",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingDetails_Brands_BrandId",
                table: "BuildingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BuildingDetails_BrandId",
                table: "BuildingDetails");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "BuildingDetails");
        }
    }
}
