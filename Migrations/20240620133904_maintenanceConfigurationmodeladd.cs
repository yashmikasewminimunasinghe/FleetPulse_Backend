using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class maintenanceConfigurationmodeladd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                table: "VehicleMaintenanceConfigurations");

            migrationBuilder.RenameColumn(
                name: "DeviceToken",
                table: "VehicleMaintenanceConfigurations",
                newName: "TypeName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "VehicleMaintenanceConfigurations",
                newName: "DeviceToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                table: "VehicleMaintenanceConfigurations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
