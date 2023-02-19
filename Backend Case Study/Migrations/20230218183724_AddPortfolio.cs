using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend_Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class AddPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Shares",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Portfolios_PortfolioId",
                table: "Shares");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Shares_PortfolioId",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Shares");
        }
    }
}
