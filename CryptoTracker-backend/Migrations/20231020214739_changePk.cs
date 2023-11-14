using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class changePk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_CoinsInAlerts_CoinId",
                table: "Alerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinsInAlerts",
                table: "CoinsInAlerts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoinsInAlerts");

            migrationBuilder.RenameColumn(
                name: "CoinName",
                table: "CoinsInAlerts",
                newName: "CoinId");

            migrationBuilder.RenameIndex(
                name: "IX_CoinsInAlerts_CoinName",
                table: "CoinsInAlerts",
                newName: "IX_CoinsInAlerts_CoinId");

            migrationBuilder.AlterColumn<string>(
                name: "CoinId",
                table: "Alerts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinsInAlerts",
                table: "CoinsInAlerts",
                column: "CoinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_CoinsInAlerts_CoinId",
                table: "Alerts",
                column: "CoinId",
                principalTable: "CoinsInAlerts",
                principalColumn: "CoinId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_CoinsInAlerts_CoinId",
                table: "Alerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinsInAlerts",
                table: "CoinsInAlerts");

            migrationBuilder.RenameColumn(
                name: "CoinId",
                table: "CoinsInAlerts",
                newName: "CoinName");

            migrationBuilder.RenameIndex(
                name: "IX_CoinsInAlerts_CoinId",
                table: "CoinsInAlerts",
                newName: "IX_CoinsInAlerts_CoinName");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoinsInAlerts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "CoinId",
                table: "Alerts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinsInAlerts",
                table: "CoinsInAlerts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_CoinsInAlerts_CoinId",
                table: "Alerts",
                column: "CoinId",
                principalTable: "CoinsInAlerts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
