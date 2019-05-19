using Microsoft.EntityFrameworkCore.Migrations;

namespace Flatbuilder.DAL.Migrations
{
    public partial class FlatbuilderMigration21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Costumers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Costumers");
        }
    }
}
