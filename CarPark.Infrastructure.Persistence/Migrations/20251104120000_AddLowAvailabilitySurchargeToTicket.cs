using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPark.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLowAvailabilitySurchargeToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LowAvailabilitySurchargeApplied",
                table: "parking_tickets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowAvailabilitySurchargeApplied",
                table: "parking_tickets");
        }
    }
}
