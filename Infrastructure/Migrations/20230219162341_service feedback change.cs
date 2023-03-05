using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class servicefeedbackchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedbackDetails",
                table: "ServiceFeedbacks",
                newName: "CustomerFeedback");

            migrationBuilder.AddColumn<string>(
                name: "CompanyFeedback",
                table: "ServiceFeedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyFeedback",
                table: "ServiceFeedbacks");

            migrationBuilder.RenameColumn(
                name: "CustomerFeedback",
                table: "ServiceFeedbacks",
                newName: "FeedbackDetails");
        }
    }
}
