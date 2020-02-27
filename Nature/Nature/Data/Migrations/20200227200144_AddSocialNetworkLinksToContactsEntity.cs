using Microsoft.EntityFrameworkCore.Migrations;

namespace Nature.Data.Migrations
{
    public partial class AddSocialNetworkLinksToContactsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VKontakte",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "VKontakte",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Contacts");
        }
    }
}
