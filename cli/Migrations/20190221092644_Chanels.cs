using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cli.Migrations
{
    public partial class Chanels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Chanels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Chanels");

            migrationBuilder.DropColumn(
                name: "LinkToOriginal",
                table: "Chanels");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Chanels",
                newName: "URL");

            migrationBuilder.CreateTable(
                name: "RSSes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LinkToOriginal = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSSes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RSSes");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Chanels",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Chanels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Chanels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkToOriginal",
                table: "Chanels",
                nullable: true);
        }
    }
}
