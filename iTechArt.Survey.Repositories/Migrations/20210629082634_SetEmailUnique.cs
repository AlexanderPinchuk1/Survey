using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Survey.Repositories.Migrations
{
    public partial class SetEmailUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"),
                column: "ConcurrencyStamp",
                value: "0db5563a-63c5-4143-9602-cfa8a1d483c2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"),
                column: "ConcurrencyStamp",
                value: "d627c6ad-9ab5-41d3-a2f9-a8b1546a5b92");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "7b531830-5385-49b1-bad4-506b52facc5a", "AQAAAAEAACcQAAAAECqO9D8SB/9IJF9DKtiaw2HZIhqGSeOcsbsz9K0/M6kbAkMyXszx9AbYNDh9ne4c4g==", new DateTime(2021, 6, 29, 11, 26, 33, 890, DateTimeKind.Local).AddTicks(2457) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "7da5fbc6-dbc1-4d2c-a9ec-b7c693f6caff", "AQAAAAEAACcQAAAAEJAM/nQW/0PzswOgY4QF4567Z3FacLRDM6WpjP7Rd4lRA4dGwU7C84lEIsVmIBbv8A==", new DateTime(2021, 6, 29, 11, 26, 33, 891, DateTimeKind.Local).AddTicks(1029) });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"),
                column: "ConcurrencyStamp",
                value: "c7893c61-9fe2-457c-8191-3a353f72474e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"),
                column: "ConcurrencyStamp",
                value: "ca146645-f301-4e5e-8358-e7e0b961da0f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "c32ef7b7-6553-42fe-bc94-5749006fdc28", "AQAAAAEAACcQAAAAEDcZ4Lr0U5afXu1OSKj3TB/gGrzXXEzFrkUeOW+BGW/nzI3bxikl9otuQj7J3BLmFw==", new DateTime(2021, 6, 16, 14, 39, 53, 224, DateTimeKind.Local).AddTicks(3241) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "f0c7a123-89fa-4bf8-b67e-349e59bc038b", "AQAAAAEAACcQAAAAEMbhAw4kDJc4SMlEW+VMNCUEp8bp3PD2hvKUeYlo0N9K+AZSvHtIy8Uq4S2G7GUxnw==", new DateTime(2021, 6, 16, 14, 39, 53, 225, DateTimeKind.Local).AddTicks(127) });
        }
    }
}
