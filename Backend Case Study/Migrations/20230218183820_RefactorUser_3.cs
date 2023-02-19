using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUser_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_UserId",
                table: "Portfolios",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolios_Users_UserId",
                table: "Portfolios",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Users_UserId",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_UserId",
                table: "Portfolios");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
