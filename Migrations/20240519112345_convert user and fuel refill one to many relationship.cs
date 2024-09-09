using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class convertuserandfuelrefillonetomanyrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelRefillUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FuelRefills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FuelRefills_UserId",
                table: "FuelRefills",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuelRefills_Users_UserId",
                table: "FuelRefills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelRefills_Users_UserId",
                table: "FuelRefills");

            migrationBuilder.DropIndex(
                name: "IX_FuelRefills_UserId",
                table: "FuelRefills");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FuelRefills");

            migrationBuilder.CreateTable(
                name: "FuelRefillUsers",
                columns: table => new
                {
                    FuelRefillId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelRefillUsers", x => new { x.FuelRefillId, x.UserId });
                    table.ForeignKey(
                        name: "FK_FuelRefillUsers_FuelRefills_FuelRefillId",
                        column: x => x.FuelRefillId,
                        principalTable: "FuelRefills",
                        principalColumn: "FuelRefillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelRefillUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelRefillUsers_UserId",
                table: "FuelRefillUsers",
                column: "UserId");
        }
    }
}
