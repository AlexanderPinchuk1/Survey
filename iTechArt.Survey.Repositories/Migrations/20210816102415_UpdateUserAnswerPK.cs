using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Survey.Repositories.Migrations
{
    public partial class UpdateUserAnswerPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAnswer",
                table: "UsersAnswer");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Survey",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Question",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Page",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAnswer",
                table: "UsersAnswer",
                columns: new[] { "Id", "SurveyId", "QuestionId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAnswer",
                table: "UsersAnswer");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Survey",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Question",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Page",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAnswer",
                table: "UsersAnswer",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"),
                column: "ConcurrencyStamp",
                value: "8e8ebb5d-8f8a-445c-9860-7f6e1fc0ff65");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"),
                column: "ConcurrencyStamp",
                value: "30106184-e1d0-42f0-a51e-cc1e4d487055");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "fc07e74f-4c59-436d-9a1b-6e91c193944f", "AQAAAAEAACcQAAAAELVqKQG8RJ5J35/w2+dzFQGKzu8TZ0adjYeYyl7gpcd5WxK4LFqFUbeLFFC1kAizdg==", new DateTime(2021, 8, 1, 19, 21, 10, 814, DateTimeKind.Local).AddTicks(5207) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "8b2e125f-aaa2-45ca-9363-81505ad2ac67", "AQAAAAEAACcQAAAAEJwN5bkgoL9S6OGStFKCWAHKf/U3Ai96/QlLpP2WEf6OplslAfRE0Ypp+Z+2gCQfKg==", new DateTime(2021, 8, 1, 19, 21, 10, 815, DateTimeKind.Local).AddTicks(2485) });
        }
    }
}
