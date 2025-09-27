using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaTor.Migrations
{
    public partial class RatingAndReaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46ac8813-2cbb-4e1c-8d7d-4175ed0a5d7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b08244de-7fc6-4307-a93a-673f62c8911e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cab14009-0f7a-4255-a102-8b312c0e20e4");

            migrationBuilder.CreateTable(
                name: "ComicRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComicRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicRatings_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "ComicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserChapterReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChapterId = table.Column<int>(type: "int", nullable: false),
                    ReactionTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChapterReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChapterReactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChapterReactions_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "ChapterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChapterReactions_ReactionTypes_ReactionTypeId",
                        column: x => x.ReactionTypeId,
                        principalTable: "ReactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ComicRatings_ComicId",
                table: "ComicRatings",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicRatings_UserId",
                table: "ComicRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChapterReactions_ChapterId",
                table: "UserChapterReactions",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChapterReactions_ReactionTypeId",
                table: "UserChapterReactions",
                column: "ReactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChapterReactions_UserId_ChapterId_ReactionTypeId",
                table: "UserChapterReactions",
                columns: new[] { "UserId", "ChapterId", "ReactionTypeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "ComicRatings");

            migrationBuilder.DropTable(
                name: "UserChapterReactions");

            migrationBuilder.DropTable(
                name: "ReactionTypes");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46ac8813-2cbb-4e1c-8d7d-4175ed0a5d7f", "8fa612b3-764d-4275-bc5e-eaa34739c14f", "Admin", "ADMIN" },
                    { "b08244de-7fc6-4307-a93a-673f62c8911e", "b8a7ebfe-cda3-4d02-a5fb-93a3f08e3dcb", "User", "USER" },
                    { "cab14009-0f7a-4255-a102-8b312c0e20e4", "1de87d9f-d6eb-44e8-9151-ca44a6de10a7", "Editor", "EDITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Comics",
                keyColumn: "ComicId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2025, 9, 24, 13, 45, 25, 768, DateTimeKind.Local).AddTicks(3596));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
