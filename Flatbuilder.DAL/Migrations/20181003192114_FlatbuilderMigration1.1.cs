using Microsoft.EntityFrameworkCore.Migrations;

namespace Flatbuilder.DAL.Migrations
{
    public partial class FlatbuilderMigration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costumers_Orders_OrderId",
                table: "Costumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Orders_OrderId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_OrderId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Costumers_OrderId",
                table: "Costumers");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CostumerId",
                table: "Orders",
                column: "CostumerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Costumers_CostumerId",
                table: "Orders",
                column: "CostumerId",
                principalTable: "Costumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Costumers_CostumerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CostumerId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "Rooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OrderId1",
                table: "Rooms",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Costumers_OrderId",
                table: "Costumers",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Costumers_Orders_OrderId",
                table: "Costumers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Orders_OrderId1",
                table: "Rooms",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
