using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaTor.Migrations
{
    public partial class IdentityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f9fe8ff-d681-45ae-92b3-c9a4fd9b0db7", "4e0c1bc0-87ca-42fc-a3fc-60889a7a8903", "Editor", "EDITOR" },
                    { "5abb8f19-9de6-4ca4-a1aa-5343c6106391", "9afce2e4-cb64-417e-a752-8ae2a07dcee1", "User", "USER" },
                    { "61dc9b7a-a22a-498c-a3a0-3ed1c1eadb31", "f9a0f97a-f18e-4588-a296-6a1ed2dd758e", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 23, 16, 41, 8, 54, DateTimeKind.Local).AddTicks(4876));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f9fe8ff-d681-45ae-92b3-c9a4fd9b0db7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5abb8f19-9de6-4ca4-a1aa-5343c6106391");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61dc9b7a-a22a-498c-a3a0-3ed1c1eadb31");

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 22, 22, 30, 44, 65, DateTimeKind.Local).AddTicks(7503));
        }
    }
}
