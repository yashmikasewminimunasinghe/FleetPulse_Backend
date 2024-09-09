using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class removemaintenancestatusinvehiclemaintenancetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintenanceStatus",
                table: "VehicleMaintenances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MaintenanceStatus",
                table: "VehicleMaintenances",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
