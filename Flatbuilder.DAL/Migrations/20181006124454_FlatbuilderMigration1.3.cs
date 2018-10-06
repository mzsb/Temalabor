using Microsoft.EntityFrameworkCore.Migrations;

namespace Flatbuilder.DAL.Migrations
{
    public partial class FlatbuilderMigration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Rooms",
                newName: "RoomType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "Rooms",
                newName: "Discriminator");
        }
    }
}
