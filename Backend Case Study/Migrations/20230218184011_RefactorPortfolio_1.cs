using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class RefactorPortfolio_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolios_Users_UserId",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_UserId",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Portfolios");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PortfolioId",
                table: "Users",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Portfolios_PortfolioId",
                table: "Users",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Portfolios_PortfolioId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PortfolioId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Portfolios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
