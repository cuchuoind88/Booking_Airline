using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Airline.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration_update_FlightDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "FlightDetails",
                newName: "ArrivalTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureDate",
                table: "FlightDetails",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArrivalTime",
                table: "FlightDetails",
                newName: "ArrivalDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureDate",
                table: "FlightDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
