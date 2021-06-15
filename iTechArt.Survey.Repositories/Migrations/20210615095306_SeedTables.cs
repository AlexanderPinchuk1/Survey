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
                    { new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"), "b7775a3a-d6f9-48c8-bed1-bf097e951959", "Admin", "ADMIN" },
                    { new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"), "50c3d1c4-7328-4612-97dc-276645b601cd", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDateTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"), 0, "d4a7e29b-c5dd-465c-bc7b-0bc2072224eb", "Admin", "Admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEEnj/wX1TKRcIeCTN3u5qnJJ/Xa/HyW2ocF2jNUfHypf5c6qvmWu4x3JKz9CxhGT8w==", null, false, new DateTime(2021, 6, 15, 12, 53, 6, 402, DateTimeKind.Local).AddTicks(9873), "7d90869b-19c6-4cb0-8c74-790e8352fabe", false, "Admin@gmail.com" },
                    { new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"), 0, "1367b64a-4559-4a6b-bfbf-360d0ce146eb", "User", "User@gmail.com", false, false, null, "USER@GMAIL.COM", "USER@GMAIL.COM", "AQAAAAEAACcQAAAAECXHn6Bj902drer/7DYZuvywzIoqYUwtxZNY/iSDoSoWanpnqgDTPUKyxcIWl04CUw==", null, false, new DateTime(2021, 6, 15, 12, 53, 6, 403, DateTimeKind.Local).AddTicks(6971), "4010b6f9-59e8-42b9-bf76-97c41907189b", false, "User@gmail.com" }
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
