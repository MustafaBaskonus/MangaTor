using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaTor.Migrations
{
    public partial class updateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 22, 22, 30, 44, 65, DateTimeKind.Local).AddTicks(7503));

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 22, 21, 29, 38, 915, DateTimeKind.Local).AddTicks(6178));
        }
    }
}
