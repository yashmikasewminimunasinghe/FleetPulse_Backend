using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class VehicleMaintenanceKeyChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleMaintenances_VehicleMaintenanceId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleMaintenances",
                table: "VehicleMaintenances");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleMaintenances",
                table: "VehicleMaintenances",
                column: "MaintenanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleMaintenances_VehicleMaintenanceId",
                table: "Vehicles",
                column: "VehicleMaintenanceId",
                principalTable: "VehicleMaintenances",
                principalColumn: "MaintenanceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleMaintenances_VehicleMaintenanceId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleMaintenances",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "MaintenanceId",
                table: "VehicleMaintenances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleMaintenances",
                table: "VehicleMaintenances",
                column: "VehicleMaintenanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleMaintenances_VehicleMaintenanceId",
                table: "Vehicles",
                column: "VehicleMaintenanceId",
                principalTable: "VehicleMaintenances",
                principalColumn: "VehicleMaintenanceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
