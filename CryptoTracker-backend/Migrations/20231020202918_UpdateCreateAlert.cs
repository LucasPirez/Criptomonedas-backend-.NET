using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreateAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Alerts_UserId_CoinName",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "CoinName",
                table: "Alerts");

            migrationBuilder.AddColumn<int>(
                name: "CoinId",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_CoinId",
                table: "Alerts",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserId_CoinId",
                table: "Alerts",
                columns: new[] { "UserId", "CoinId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_CoinsInAlerts_CoinId",
                table: "Alerts",
                column: "CoinId",
                principalTable: "CoinsInAlerts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_CoinsInAlerts_CoinId",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_CoinId",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_UserId_CoinId",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "CoinId",
                table: "Alerts");

            migrationBuilder.AddColumn<string>(
                name: "CoinName",
                table: "Alerts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserId_CoinName",
                table: "Alerts",
                columns: new[] { "UserId", "CoinName" },
                unique: true);
        }
    }
}
