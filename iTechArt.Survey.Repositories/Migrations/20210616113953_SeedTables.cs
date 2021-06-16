using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Survey.Repositories.Migrations
{
    public partial class SeedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"), "c7893c61-9fe2-457c-8191-3a353f72474e", "Admin", "ADMIN" },
                    { new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"), "ca146645-f301-4e5e-8358-e7e0b961da0f", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDateTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"), 0, "c32ef7b7-6553-42fe-bc94-5749006fdc28", "Admin", "Admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEDcZ4Lr0U5afXu1OSKj3TB/gGrzXXEzFrkUeOW+BGW/nzI3bxikl9otuQj7J3BLmFw==", null, false, new DateTime(2021, 6, 16, 14, 39, 53, 224, DateTimeKind.Local).AddTicks(3241), "7d90869b-19c6-4cb0-8c74-790e8352fabe", false, "Admin@gmail.com" },
                    { new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"), 0, "f0c7a123-89fa-4bf8-b67e-349e59bc038b", "User", "User@gmail.com", false, false, null, "USER@GMAIL.COM", "USER@GMAIL.COM", "AQAAAAEAACcQAAAAEMbhAw4kDJc4SMlEW+VMNCUEp8bp3PD2hvKUeYlo0N9K+AZSvHtIy8Uq4S2G7GUxnw==", null, false, new DateTime(2021, 6, 16, 14, 39, 53, 225, DateTimeKind.Local).AddTicks(127), "4010b6f9-59e8-42b9-bf76-97c41907189b", false, "User@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 2, "DisplayName", "Admin", new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872") },
                    { 1, "DisplayName", "User", new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"), new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872") },
                    { new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"), new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"), new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"), new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"));
        }
    }
}
