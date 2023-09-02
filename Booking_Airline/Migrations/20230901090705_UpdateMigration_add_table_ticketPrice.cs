using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Airline.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration_add_table_ticketPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "SeatDetails");

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "SeatDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TicketPrice",
                columns: table => new
                {
                    TicketPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPrice", x => x.TicketPriceId);
                    table.ForeignKey(
                        name: "FK_TicketPrice_FlightDetails_FlightId",
                        column: x => x.FlightId,
                        principalTable: "FlightDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketPrice_TravelClasses_ClassID",
                        column: x => x.ClassID,
                        principalTable: "TravelClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketPrice_ClassID",
                table: "TicketPrice",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPrice_FlightId",
                table: "TicketPrice",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPrice");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "SeatDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "TicketPrice",
                table: "SeatDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
