using Microsoft.EntityFrameworkCore.Migrations;

namespace Nature.Data.Migrations
{
    public partial class AddContactsForeignKeyToAboutUsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactsId",
                table: "AboutUs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AboutUs_ContactsId",
                table: "AboutUs",
                column: "ContactsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutUs_Contacts_ContactsId",
                table: "AboutUs",
                column: "ContactsId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutUs_Contacts_ContactsId",
                table: "AboutUs");

            migrationBuilder.DropIndex(
                name: "IX_AboutUs_ContactsId",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "ContactsId",
                table: "AboutUs");
        }
    }
}
