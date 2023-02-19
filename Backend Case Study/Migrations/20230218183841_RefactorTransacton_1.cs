using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Case_Study.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTransacton_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ShareId",
                table: "Transactions",
                column: "ShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Shares_ShareId",
                table: "Transactions",
                column: "ShareId",
                principalTable: "Shares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Shares_ShareId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ShareId",
                table: "Transactions");
        }
    }
}
