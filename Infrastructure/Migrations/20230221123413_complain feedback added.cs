using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class complainfeedbackadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplainFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionTakenAgainstComplain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTakenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComplainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplainFeedback_Complains_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "Complains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplainFeedback_ComplainId",
                table: "ComplainFeedback",
                column: "ComplainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplainFeedback");
        }
    }
}
