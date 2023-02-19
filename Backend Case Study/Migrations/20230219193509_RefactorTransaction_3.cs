using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTransaction_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShareId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "ShareSymbol",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShareSymbol",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "ShareId",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
