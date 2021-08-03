using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Survey.Repositories.Migrations
{
    public partial class UpdateUserAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAnswer",
                table: "UsersAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnswer_QuestionId",
                table: "UsersAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnswer_SurveyId",
                table: "UsersAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnswer_UserId",
                table: "UsersAnswer");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UsersAnswer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UsersAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newsequentialid()");

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

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnswer_QuestionId",
                table: "UsersAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnswer_SurveyId",
                table: "UsersAnswer",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnswer_UserId",
                table: "UsersAnswer",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAnswer",
                table: "UsersAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnswer_QuestionId",
                table: "UsersAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnswer_SurveyId",
                table: "UsersAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UsersAnswer_UserId",
                table: "UsersAnswer");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsersAnswer");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UsersAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAnswer",
                table: "UsersAnswer",
                columns: new[] { "SurveyId", "UserId", "QuestionId" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"),
                column: "ConcurrencyStamp",
                value: "c9f2d69b-bd9b-4838-abec-ee9b78029431");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"),
                column: "ConcurrencyStamp",
                value: "a2bd8fca-886a-4f3c-87dd-ff76fb6b04f1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "be2e9682-ceb4-4ab7-9bb0-8d75ae7934e9", "AQAAAAEAACcQAAAAEBic5pf7refnwLyapk4lvQMwwyRe1jDRn5qTXJCLo0QhShFGJFFphAb/l1RHpdgHtQ==", new DateTime(2021, 7, 9, 14, 47, 40, 978, DateTimeKind.Local).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "c6c94d94-f225-4204-985f-88a3463cc576", "AQAAAAEAACcQAAAAEIkIriOiFXHJNrkW+hG0cwCwE+pTT/mRo4zMZpm6qWp0A+XcAPijFJPxXZEvTIfeAw==", new DateTime(2021, 7, 9, 14, 47, 40, 978, DateTimeKind.Local).AddTicks(7958) });

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnswer_QuestionId",
                table: "UsersAnswer",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnswer_SurveyId",
                table: "UsersAnswer",
                column: "SurveyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnswer_UserId",
                table: "UsersAnswer",
                column: "UserId",
                unique: true);
        }
    }
}
