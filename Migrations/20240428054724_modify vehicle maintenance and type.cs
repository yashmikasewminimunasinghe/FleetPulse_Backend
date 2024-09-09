using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class modifyvehiclemaintenanceandtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Accidents_AccidentId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_FuelRefills_FuelRefillId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Trips_TripId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleMaintenances_VehicleMaintenanceId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_AccidentId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_FuelRefillId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TripId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleMaintenanceId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AccidentId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "FuelRefillId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleMaintenanceId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccidentVehicle",
                columns: table => new
                {
                    AccidentId = table.Column<int>(type: "int", nullable: false),
                    VehiclesVehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentVehicle", x => new { x.AccidentId, x.VehiclesVehicleId });
                    table.ForeignKey(
                        name: "FK_AccidentVehicle_Accidents_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accidents",
                        principalColumn: "AccidentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccidentVehicle_Vehicles_VehiclesVehicleId",
                        column: x => x.VehiclesVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripVehicle",
                columns: table => new
                {
                    TripId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehiclesVehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripVehicle", x => new { x.TripId, x.VehiclesVehicleId });
                    table.ForeignKey(
                        name: "FK_TripVehicle_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripVehicle_Vehicles_VehiclesVehicleId",
                        column: x => x.VehiclesVehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_VehicleId",
                table: "VehicleMaintenances",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccidentVehicle_VehiclesVehicleId",
                table: "AccidentVehicle",
                column: "VehiclesVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_TripVehicle_VehiclesVehicleId",
                table: "TripVehicle",
                column: "VehiclesVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_Vehicles_VehicleId",
                table: "VehicleMaintenances",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_Vehicles_VehicleId",
                table: "VehicleMaintenances");

            migrationBuilder.DropTable(
                name: "AccidentVehicle");

            migrationBuilder.DropTable(
                name: "TripVehicle");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_VehicleId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "VehicleMaintenances");

            migrationBuilder.AddColumn<int>(
                name: "AccidentId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelRefillId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TripId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VehicleMaintenanceId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AccidentId",
                table: "Vehicles",
                column: "AccidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FuelRefillId",
                table: "Vehicles",
                column: "FuelRefillId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TripId",
                table: "Vehicles",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleMaintenanceId",
                table: "Vehicles",
                column: "VehicleMaintenanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Accidents_AccidentId",
                table: "Vehicles",
                column: "AccidentId",
                principalTable: "Accidents",
                principalColumn: "AccidentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_FuelRefills_FuelRefillId",
                table: "Vehicles",
                column: "FuelRefillId",
                principalTable: "FuelRefills",
                principalColumn: "FuelRefillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Trips_TripId",
                table: "Vehicles",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleMaintenances_VehicleMaintenanceId",
                table: "Vehicles",
                column: "VehicleMaintenanceId",
                principalTable: "VehicleMaintenances",
                principalColumn: "MaintenanceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
