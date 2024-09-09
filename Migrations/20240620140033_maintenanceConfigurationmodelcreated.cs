using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class maintenanceConfigurationmodelcreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenanceConfigurations_VehicleMaintenanceTypes_VehicleMaintenanceTypeId",
                table: "VehicleMaintenanceConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenanceConfigurations_VehicleMaintenanceTypeId",
                table: "VehicleMaintenanceConfigurations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenanceConfigurations_VehicleMaintenanceTypeId",
                table: "VehicleMaintenanceConfigurations",
                column: "VehicleMaintenanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenanceConfigurations_VehicleMaintenanceTypes_VehicleMaintenanceTypeId",
                table: "VehicleMaintenanceConfigurations",
                column: "VehicleMaintenanceTypeId",
                principalTable: "VehicleMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
