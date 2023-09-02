using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Airline.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration_2_9_2023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatDetails_FlightDetails_FilghtId",
                table: "SeatDetails");

            migrationBuilder.RenameColumn(
                name: "FilghtId",
                table: "SeatDetails",
                newName: "SeatAdditionalServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SeatDetails_FilghtId",
                table: "SeatDetails",
                newName: "IX_SeatDetails_SeatAdditionalServiceId");

            migrationBuilder.AddColumn<string>(
                name: "ServcieDescription",
                table: "ServiceForClasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "SeatDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SeatCode",
                table: "SeatDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AddionalFoodService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddionalFoodService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalSeatService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seatLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seatType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seatPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalSeatService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddionalFoodServiceFlightDetail",
                columns: table => new
                {
                    AddionalFoodServicesId = table.Column<int>(type: "int", nullable: false),
                    flightDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddionalFoodServiceFlightDetail", x => new { x.AddionalFoodServicesId, x.flightDetailsId });
                    table.ForeignKey(
                        name: "FK_AddionalFoodServiceFlightDetail_AddionalFoodService_AddionalFoodServicesId",
                        column: x => x.AddionalFoodServicesId,
                        principalTable: "AddionalFoodService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddionalFoodServiceFlightDetail_FlightDetails_flightDetailsId",
                        column: x => x.flightDetailsId,
                        principalTable: "FlightDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddionalFoodServiceReservation",
                columns: table => new
                {
                    AddionalFoodServicesId = table.Column<int>(type: "int", nullable: false),
                    reservationDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddionalFoodServiceReservation", x => new { x.AddionalFoodServicesId, x.reservationDetailsId });
                    table.ForeignKey(
                        name: "FK_AddionalFoodServiceReservation_AddionalFoodService_AddionalFoodServicesId",
                        column: x => x.AddionalFoodServicesId,
                        principalTable: "AddionalFoodService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddionalFoodServiceReservation_Reservations_reservationDetailsId",
                        column: x => x.reservationDetailsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodForFlight",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    FoodServiceId = table.Column<int>(type: "int", nullable: false),
                    FlightDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodForFlight", x => new { x.FlightId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_FoodForFlight_AddionalFoodService_FoodServiceId",
                        column: x => x.FoodServiceId,
                        principalTable: "AddionalFoodService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodForFlight_FlightDetails_FlightDetailId",
                        column: x => x.FlightDetailId,
                        principalTable: "FlightDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationMapAddionalFoodService",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    AdditionalFoodServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationMapAddionalFoodService", x => new { x.ReservationId, x.AdditionalFoodServiceId });
                    table.ForeignKey(
                        name: "FK_ReservationMapAddionalFoodService_AddionalFoodService_AdditionalFoodServiceId",
                        column: x => x.AdditionalFoodServiceId,
                        principalTable: "AddionalFoodService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationMapAddionalFoodService_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatDetails_FlightId",
                table: "SeatDetails",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_AddionalFoodServiceFlightDetail_flightDetailsId",
                table: "AddionalFoodServiceFlightDetail",
                column: "flightDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AddionalFoodServiceReservation_reservationDetailsId",
                table: "AddionalFoodServiceReservation",
                column: "reservationDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodForFlight_FlightDetailId",
                table: "FoodForFlight",
                column: "FlightDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodForFlight_FoodServiceId",
                table: "FoodForFlight",
                column: "FoodServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationMapAddionalFoodService_AdditionalFoodServiceId",
                table: "ReservationMapAddionalFoodService",
                column: "AdditionalFoodServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatDetails_AdditionalSeatService_SeatAdditionalServiceId",
                table: "SeatDetails",
                column: "SeatAdditionalServiceId",
                principalTable: "AdditionalSeatService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatDetails_FlightDetails_FlightId",
                table: "SeatDetails",
                column: "FlightId",
                principalTable: "FlightDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatDetails_AdditionalSeatService_SeatAdditionalServiceId",
                table: "SeatDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatDetails_FlightDetails_FlightId",
                table: "SeatDetails");

            migrationBuilder.DropTable(
                name: "AddionalFoodServiceFlightDetail");

            migrationBuilder.DropTable(
                name: "AddionalFoodServiceReservation");

            migrationBuilder.DropTable(
                name: "AdditionalSeatService");

            migrationBuilder.DropTable(
                name: "FoodForFlight");

            migrationBuilder.DropTable(
                name: "ReservationMapAddionalFoodService");

            migrationBuilder.DropTable(
                name: "AddionalFoodService");

            migrationBuilder.DropIndex(
                name: "IX_SeatDetails_FlightId",
                table: "SeatDetails");

            migrationBuilder.DropColumn(
                name: "ServcieDescription",
                table: "ServiceForClasses");

            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "SeatDetails");

            migrationBuilder.DropColumn(
                name: "SeatCode",
                table: "SeatDetails");

            migrationBuilder.RenameColumn(
                name: "SeatAdditionalServiceId",
                table: "SeatDetails",
                newName: "FilghtId");

            migrationBuilder.RenameIndex(
                name: "IX_SeatDetails_SeatAdditionalServiceId",
                table: "SeatDetails",
                newName: "IX_SeatDetails_FilghtId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatDetails_FlightDetails_FilghtId",
                table: "SeatDetails",
                column: "FilghtId",
                principalTable: "FlightDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
