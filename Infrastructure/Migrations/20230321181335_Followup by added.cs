using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Followupbyadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FollowupById",
                table: "Followups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Followups_FollowupById",
                table: "Followups",
                column: "FollowupById");

            migrationBuilder.AddForeignKey(
                name: "FK_Followups_Employees_FollowupById",
                table: "Followups",
                column: "FollowupById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followups_Employees_FollowupById",
                table: "Followups");

            migrationBuilder.DropIndex(
                name: "IX_Followups_FollowupById",
                table: "Followups");

            migrationBuilder.DropColumn(
                name: "FollowupById",
                table: "Followups");
        }
    }
}
