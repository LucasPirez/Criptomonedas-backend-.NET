using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_UserDatas_UserDataId",
                table: "Alerts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserDatas_UserDataId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserDatas");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserDataId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "UserDataId",
                table: "Alerts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Alerts_UserDataId",
                table: "Alerts",
                newName: "IX_Alerts_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Age",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UsersCredentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersCredentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersCredentials_UserId",
                table: "UsersCredentials",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_Users_UserId",
                table: "Alerts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerts_Users_UserId",
                table: "Alerts");

            migrationBuilder.DropTable(
                name: "UsersCredentials");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Alerts",
                newName: "UserDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Alerts_UserId",
                table: "Alerts",
                newName: "IX_Alerts_UserDataId");

            migrationBuilder.AddColumn<int>(
                name: "UserDataId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDatas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserDataId",
                table: "Users",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerts_UserDatas_UserDataId",
                table: "Alerts",
                column: "UserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserDatas_UserDataId",
                table: "Users",
                column: "UserDataId",
                principalTable: "UserDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
