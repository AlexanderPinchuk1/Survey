using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Survey.Repositories.Migrations
{
    public partial class UpdateUserAnswerAndSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "Survey",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"),
                column: "ConcurrencyStamp",
                value: "ecb6e2b1-0702-46f8-93c8-cdacc9c9d08a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"),
                column: "ConcurrencyStamp",
                value: "bfcde554-a8ea-4d39-9239-b4faca2cc965");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "9af9d967-be55-464b-a6d6-c1c5cac89ece", "AQAAAAEAACcQAAAAEJgdRq9e5QLFz8zRlyYSjnL+QpP3IM1T3NKfBVhX6N4pmxSP8uxIkzZwAbOxTMmXsg==", new DateTime(2021, 8, 18, 12, 46, 33, 960, DateTimeKind.Local).AddTicks(9811) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "d376b521-2d9a-4d13-a70d-35fd7389f2df", "AQAAAAEAACcQAAAAEP8R18yBREnVZSDDrftq9vaj9SSj/VJJ4oS14SbAxHetxPAQEHC4mT67n5bHFaWY+g==", new DateTime(2021, 8, 18, 12, 46, 33, 961, DateTimeKind.Local).AddTicks(6217) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "Survey");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"),
                column: "ConcurrencyStamp",
                value: "af7902cb-13f6-4f53-999c-c62289e5113c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"),
                column: "ConcurrencyStamp",
                value: "a38b53b5-33a5-49dd-a234-4f5f994feb99");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "04eb4112-2528-417d-ac9a-9949b992a8e3", "AQAAAAEAACcQAAAAEHGY6mBzpH7T9H+fKaMabg2JPG1rx/gQnT2MsXTYMCCWmzibvz+2prWyjlNN45S0jg==", new DateTime(2021, 8, 16, 13, 24, 14, 716, DateTimeKind.Local).AddTicks(5937) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "d0ee0ea6-f700-4d6f-814c-fd837772c591", "AQAAAAEAACcQAAAAENDoJRGwf3qR2TxM7mp7VbTpfBvNx5VZtIGuDM0OHamILe2j/e1j6FAr1B0YWoAqlg==", new DateTime(2021, 8, 16, 13, 24, 14, 717, DateTimeKind.Local).AddTicks(3220) });
        }
    }
}
