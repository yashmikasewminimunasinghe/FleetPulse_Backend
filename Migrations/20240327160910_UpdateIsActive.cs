using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class UpdateIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "VerificationCodes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VerificationCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VerificationCodes");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VerificationCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
