using Microsoft.EntityFrameworkCore.Migrations;

namespace Flatbuilder.DAL.Migrations
{
    public partial class CostumerPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Costumers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Costumers");
        }
    }
}
