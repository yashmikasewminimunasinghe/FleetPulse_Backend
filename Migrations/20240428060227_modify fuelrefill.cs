using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class modifyfuelrefill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "FuelRefills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FuelRefills_VehicleId",
                table: "FuelRefills",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuelRefills_Vehicles_VehicleId",
                table: "FuelRefills",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelRefills_Vehicles_VehicleId",
                table: "FuelRefills");

            migrationBuilder.DropIndex(
                name: "IX_FuelRefills_VehicleId",
                table: "FuelRefills");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "FuelRefills");
        }
    }
}
