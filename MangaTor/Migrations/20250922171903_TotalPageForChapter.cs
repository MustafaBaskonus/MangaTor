using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaTor.Migrations
{
    public partial class TotalPageForChapter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPae",
                table: "Chapters",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 22, 20, 19, 3, 351, DateTimeKind.Local).AddTicks(9608));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPae",
                table: "Chapters");

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 21, 17, 37, 41, 558, DateTimeKind.Local).AddTicks(3890));
        }
    }
}
