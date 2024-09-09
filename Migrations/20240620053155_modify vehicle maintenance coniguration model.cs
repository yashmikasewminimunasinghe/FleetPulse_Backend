using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class modifyvehiclemaintenanceconigurationmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenanceConfigurations_Vehicles_VehicleId",
                table: "VehicleMaintenanceConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenanceConfigurations_VehicleId",
                table: "VehicleMaintenanceConfigurations");

            migrationBuilder.AddColumn<string>(
                name: "VehicleRegistrationNo",
                table: "VehicleMaintenanceConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleRegistrationNo",
                table: "VehicleMaintenanceConfigurations");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenanceConfigurations_VehicleId",
                table: "VehicleMaintenanceConfigurations",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenanceConfigurations_Vehicles_VehicleId",
                table: "VehicleMaintenanceConfigurations",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
