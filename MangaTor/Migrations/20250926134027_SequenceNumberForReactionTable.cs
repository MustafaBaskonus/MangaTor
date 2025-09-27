using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaTor.Migrations
{
    public partial class SequenceNumberForReactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56c6bbbc-4c83-43be-9786-79c6f9faf0af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdf815ad-51c3-4d16-9a3e-9ec3b9adfc00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1aa6981-0289-41a7-ae18-f301e989e168");

            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "ReactionTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "487fafa8-acad-4cd3-9c44-bc2efb02b2b2", "c30e97fd-1b40-4f59-85b4-3b4a2c981767", "Editor", "EDITOR" },
                    { "a712c626-9b31-4024-8081-e64f57c3e977", "50185400-79d0-46d0-b6be-879fe6968d43", "Admin", "ADMIN" },
                    { "e959fdaf-a39d-4613-8987-ae2c8f583465", "2bc25e02-bcb4-485d-8ae3-8414c668c559", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 26, 16, 40, 27, 277, DateTimeKind.Local).AddTicks(688));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "487fafa8-acad-4cd3-9c44-bc2efb02b2b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a712c626-9b31-4024-8081-e64f57c3e977");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e959fdaf-a39d-4613-8987-ae2c8f583465");

            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "ReactionTypes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56c6bbbc-4c83-43be-9786-79c6f9faf0af", "6a0a8370-7acd-4f8f-8d50-385b35512441", "User", "USER" },
                    { "bdf815ad-51c3-4d16-9a3e-9ec3b9adfc00", "f53c7ded-e67a-441d-8ac3-5ca72a4a2025", "Admin", "ADMIN" },
                    { "c1aa6981-0289-41a7-ae18-f301e989e168", "4c87e252-c05c-439a-b1b4-3976429cc0aa", "Editor", "EDITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 26, 14, 49, 25, 817, DateTimeKind.Local).AddTicks(9462));
        }
    }
}
