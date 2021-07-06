﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTechArt.Survey.Repositories.Migrations
{
    public partial class CreateSurveyDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsTemplate = table.Column<bool>(type: "bit", nullable: false),
                    Options = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Survey_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResult",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResult", x => new { x.UserId, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_SurveyResult_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyResult_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Missed = table.Column<int>(type: "int", nullable: false),
                    AvailableAnswers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionTypeId = table.Column<byte>(type: "tinyint", nullable: false),
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_QuestionType_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersAnswer",
                columns: table => new
                {
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAnswer", x => new { x.SurveyId, x.UserId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_UsersAnswer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersAnswer_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersAnswer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "QuestionType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Text" },
                    { (byte)2, "File" },
                    { (byte)3, "OneAnswer" },
                    { (byte)4, "ManyAnswers" },
                    { (byte)5, "Scale" },
                    { (byte)6, "Rating" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ac23da-a8aa-47b4-a2a8-d32457760489"),
                column: "ConcurrencyStamp",
                value: "1ab35110-4954-4c57-8f24-f303eb438ba8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aed7daac-9ce0-496f-a606-7b79d37dcbc1"),
                column: "ConcurrencyStamp",
                value: "32208f27-ad96-4a59-a7ca-a2d2c81b61ca");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1f363ed7-59b2-460c-91a6-fcd30a2c3872"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "fb8a4db1-5525-49e9-806b-f21c486ce42c", "AQAAAAEAACcQAAAAEE6tA/nQXRG7V8DrfhYwmN6xjFT8+7ME0xyBz0cqiMYNIPEwfgQozBYBMABNJfTaEQ==", new DateTime(2021, 7, 6, 19, 37, 21, 385, DateTimeKind.Local).AddTicks(1385) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ba77241-b5d6-4490-aa85-0493c6acdbf3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationDateTime" },
                values: new object[] { "448ef47a-dbff-497b-a9a5-9552504d9734", "AQAAAAEAACcQAAAAEA02lMpUeQ3FLPDFhDwiNUdT5Ty+k2ZT5MDmZoJvIw656nzbVLMVWv1M/F8t0o17ww==", new DateTime(2021, 7, 6, 19, 37, 21, 385, DateTimeKind.Local).AddTicks(8157) });

            migrationBuilder.CreateIndex(
                name: "IX_Page_SurveyId",
                table: "Page",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_PageId",
                table: "Question",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionType_Name",
                table: "QuestionType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Survey_CreatedById",
                table: "Survey",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResult_SurveyId",
                table: "SurveyResult",
                column: "SurveyId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyResult");

            migrationBuilder.DropTable(
                name: "UsersAnswer");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "QuestionType");

            migrationBuilder.DropTable(
                name: "Survey");

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
        }
    }
}
