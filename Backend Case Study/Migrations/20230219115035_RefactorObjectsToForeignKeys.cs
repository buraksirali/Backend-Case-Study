using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class RefactorObjectsToForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Portfolios_PortfolioId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Shares_ShareId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Portfolios_PortfolioId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PortfolioId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ShareId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Shares_PortfolioId",
                table: "Shares");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Shares",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "Shares",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PortfolioId",
                table: "Users",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ShareId",
                table: "Transactions",
                column: "ShareId");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_PortfolioId",
                table: "Shares",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Portfolios_PortfolioId",
                table: "Shares",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Shares_ShareId",
                table: "Transactions",
                column: "ShareId",
                principalTable: "Shares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Portfolios_PortfolioId",
                table: "Users",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id");
        }
    }
}
