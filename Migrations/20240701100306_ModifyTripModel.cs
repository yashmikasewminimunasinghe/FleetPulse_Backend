using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class ModifyTripModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleModel_VehicleModelId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleModelId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleModelId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleColor",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "AccidentId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuelRefillId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TripId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accidents",
                columns: table => new
                {
                    AccidentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Venue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photos = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    SpecialNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Loss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverInjuredStatus = table.Column<bool>(type: "bit", nullable: false),
                    HelperInjuredStatus = table.Column<bool>(type: "bit", nullable: false),
                    VehicleDamagedStatus = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    HelperId = table.Column<int>(type: "int", nullable: false),
                    LossStatement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.AccidentId);
                });

            migrationBuilder.CreateTable(
                name: "FuelRefillUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FuelRefillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelRefillUsers", x => new { x.UserId, x.FuelRefillId });
                    table.ForeignKey(
                        name: "FK_FuelRefillUsers_FuelRefills_FuelRefillId",
                        column: x => x.FuelRefillId,
                        principalTable: "FuelRefills",
                        principalColumn: "FuelRefillId");
                    table.ForeignKey(
                        name: "FK_FuelRefillUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartMeterValue = table.Column<float>(type: "real", nullable: false),
                    EndMeterValue = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                });

            migrationBuilder.CreateTable(
                name: "AccidentUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccidentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentUsers", x => new { x.AccidentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AccidentUsers_Accidents_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accidents",
                        principalColumn: "AccidentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccidentUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TripUserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripUsers", x => new { x.TripId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TripUsers_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AccidentUsers_UserId",
                table: "AccidentUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelRefillUsers_FuelRefillId",
                table: "FuelRefillUsers",
                column: "FuelRefillId");

            migrationBuilder.CreateIndex(
                name: "IX_TripUsers_UserId",
                table: "TripUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Accidents_AccidentId",
                table: "Vehicles",
                column: "AccidentId",
                principalTable: "Accidents",
                principalColumn: "AccidentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_FuelRefills_FuelRefillId",
                table: "Vehicles",
                column: "FuelRefillId",
                principalTable: "FuelRefills",
                principalColumn: "FuelRefillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Trips_TripId",
                table: "Vehicles",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "AccidentUsers");

            migrationBuilder.DropTable(
                name: "FuelRefillUsers");

            migrationBuilder.DropTable(
                name: "TripUsers");

            migrationBuilder.DropTable(
                name: "Accidents");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_AccidentId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_FuelRefillId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TripId",
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

            migrationBuilder.AlterColumn<string>(
                name: "VehicleColor",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleModelId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    VehicleModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.VehicleModelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleModelId",
                table: "Vehicles",
                column: "VehicleModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleModel_VehicleModelId",
                table: "Vehicles",
                column: "VehicleModelId",
                principalTable: "VehicleModel",
                principalColumn: "VehicleModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
