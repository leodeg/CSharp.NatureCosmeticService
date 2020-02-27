using Microsoft.EntityFrameworkCore.Migrations;

namespace Nature.Data.Migrations
{
    public partial class AddTouTubeLinkToContactsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YouTube",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YouTube",
                table: "Contacts");
        }
    }
}
