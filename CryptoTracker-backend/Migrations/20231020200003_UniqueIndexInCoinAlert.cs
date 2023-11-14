using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndexInCoinAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CoinName",
                table: "CoinsInAlerts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CoinsInAlerts_CoinName",
                table: "CoinsInAlerts",
                column: "CoinName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CoinsInAlerts_CoinName",
                table: "CoinsInAlerts");

            migrationBuilder.AlterColumn<string>(
                name: "CoinName",
                table: "CoinsInAlerts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
