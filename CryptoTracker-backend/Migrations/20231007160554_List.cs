using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class List : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDataId",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserDataId",
                table: "Alerts",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_UserDatas_UserDataId",
                table: "Alerts",
                column: "UserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_UserDatas_UserDataId",
                table: "Alerts");

            migrationBuilder.DropIndex(
                name: "IX_Alerts_UserDataId",
                table: "Alerts");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Alerts");
        }
    }
}
