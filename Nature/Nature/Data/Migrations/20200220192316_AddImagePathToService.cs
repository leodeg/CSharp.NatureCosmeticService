using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nature.Data.Migrations
{
    public partial class AddImagePathToService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Services",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Services");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Services",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
