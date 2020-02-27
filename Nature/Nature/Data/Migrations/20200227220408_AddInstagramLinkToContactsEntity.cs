using Microsoft.EntityFrameworkCore.Migrations;

namespace Nature.Data.Migrations
{
    public partial class AddInstagramLinkToContactsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Contacts");
        }
    }
}
