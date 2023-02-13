using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Changecustomerentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ServiceTypes_ServiceTypeId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ServiceTypeId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerDoTheWorkingMonth",
                table: "Followups",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "Followups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MonthList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthList", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MonthList",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "January" },
                    { 2, "February" },
                    { 3, "March" },
                    { 4, "April" },
                    { 5, "May" },
                    { 6, "June" },
                    { 7, "July" },
                    { 8, "August" },
                    { 9, "September" },
                    { 10, "October" },
                    { 11, "November" },
                    { 12, "December" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Followups_ServiceTypeId",
                table: "Followups",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Followups_ServiceTypes_ServiceTypeId",
                table: "Followups",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followups_ServiceTypes_ServiceTypeId",
                table: "Followups");

            migrationBuilder.DropTable(
                name: "MonthList");

            migrationBuilder.DropIndex(
                name: "IX_Followups_ServiceTypeId",
                table: "Followups");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "Followups");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerDoTheWorkingMonth",
                table: "Followups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ServiceTypeId",
                table: "Customers",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ServiceTypes_ServiceTypeId",
                table: "Customers",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
