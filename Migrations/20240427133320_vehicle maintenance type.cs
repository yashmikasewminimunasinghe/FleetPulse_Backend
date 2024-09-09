using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class vehiclemaintenancetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_Id",
                table: "VehicleMaintenances");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VehicleMaintenances",
                newName: "VehicleMaintenanceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleMaintenances_Id",
                table: "VehicleMaintenances",
                newName: "IX_VehicleMaintenances_VehicleMaintenanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_VehicleMaintenanceTypeId",
                table: "VehicleMaintenances",
                column: "VehicleMaintenanceTypeId",
                principalTable: "VehicleMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_VehicleMaintenanceTypeId",
                table: "VehicleMaintenances");

            migrationBuilder.RenameColumn(
                name: "VehicleMaintenanceTypeId",
                table: "VehicleMaintenances",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleMaintenances_VehicleMaintenanceTypeId",
                table: "VehicleMaintenances",
                newName: "IX_VehicleMaintenances_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_Id",
                table: "VehicleMaintenances",
                column: "Id",
                principalTable: "VehicleMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
