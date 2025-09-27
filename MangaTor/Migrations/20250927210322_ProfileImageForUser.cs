using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaTor.Migrations
{
    public partial class ProfileImageForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62fe917c-9090-4a5d-b2f2-580ab045a36b", "b8e90227-3170-43aa-a0be-9f8a4c861bc0", "User", "USER" },
                    { "c2c219d1-1242-46bc-9265-5f2877d59019", "5428ea77-4b1b-4eec-8a50-417e9be961cb", "Editor", "EDITOR" },
                    { "c957441c-707f-4f7a-96e6-70f6f011d0e4", "e7bd788d-02ab-4dc4-85d6-e031e2f94b43", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 28, 0, 3, 22, 14, DateTimeKind.Local).AddTicks(9348));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62fe917c-9090-4a5d-b2f2-580ab045a36b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2c219d1-1242-46bc-9265-5f2877d59019");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c957441c-707f-4f7a-96e6-70f6f011d0e4");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "AspNetUsers");

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
    }
}
