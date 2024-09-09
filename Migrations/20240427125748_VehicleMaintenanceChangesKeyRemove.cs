using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class VehicleMaintenanceChangesKeyRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleMaintenanceId",
                table: "VehicleMaintenances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleMaintenanceId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
