using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Airline.Migrations
{
    /// <inheritdoc />
    public partial class _1123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddionalFoodService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    seatLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seatType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seatPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalSeatService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    contryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    countryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceForClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServcieDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceForClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TravelClassName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerifyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirportCity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airports_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceForClassTravelClass",
                columns: table => new
                {
                    ServiceForClassesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TravelClassesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceForClassTravelClass", x => new { x.ServiceForClassesId, x.TravelClassesId });
                    table.ForeignKey(
                        name: "FK_ServiceForClassTravelClass_ServiceForClasses_ServiceForClassesId",
                        column: x => x.ServiceForClassesId,
                        principalTable: "ServiceForClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceForClassTravelClass_TravelClasses_TravelClassesId",
                        column: x => x.TravelClassesId,
                        principalTable: "TravelClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleModelUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModelUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleModelUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleModelUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenRemainLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenRemainLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenRemainLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceAirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationAirportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilghtName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirlineType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightDetails_Airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightDetails_Airports_SourceAirportId",
                        column: x => x.SourceAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddionalFoodServiceFlightDetail",
                columns: table => new
                {
                    AddionalFoodServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flightDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "FoodForFlight",
                columns: table => new
                {
                    FoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FoodServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "SeatDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatAdditionalServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatDetails_AdditionalSeatService_SeatAdditionalServiceId",
                        column: x => x.SeatAdditionalServiceId,
                        principalTable: "AdditionalSeatService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatDetails_FlightDetails_FlightId",
                        column: x => x.FlightId,
                        principalTable: "FlightDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatDetails_TravelClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "TravelClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketPrice",
                columns: table => new
                {
                    TicketPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengerIDId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatDetailsIDId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfReservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RervationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Passengers_PassengerIDId",
                        column: x => x.PassengerIDId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_SeatDetails_SeatDetailsIDId",
                        column: x => x.SeatDetailsIDId,
                        principalTable: "SeatDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddionalFoodServiceReservation",
                columns: table => new
                {
                    AddionalFoodServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reservationDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationIDId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Reservations_ReservationIDId",
                        column: x => x.ReservationIDId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationMapAddionalFoodService",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalFoodServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfMeals = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_AddionalFoodServiceFlightDetail_flightDetailsId",
                table: "AddionalFoodServiceFlightDetail",
                column: "flightDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AddionalFoodServiceReservation_reservationDetailsId",
                table: "AddionalFoodServiceReservation",
                column: "reservationDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_CountryId",
                table: "Airports",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightDetails_DestinationAirportId",
                table: "FlightDetails",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightDetails_SourceAirportId",
                table: "FlightDetails",
                column: "SourceAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodForFlight_FlightDetailId",
                table: "FoodForFlight",
                column: "FlightDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodForFlight_FoodServiceId",
                table: "FoodForFlight",
                column: "FoodServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReservationIDId",
                table: "Payments",
                column: "ReservationIDId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationMapAddionalFoodService_AdditionalFoodServiceId",
                table: "ReservationMapAddionalFoodService",
                column: "AdditionalFoodServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PassengerIDId",
                table: "Reservations",
                column: "PassengerIDId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatDetailsIDId",
                table: "Reservations",
                column: "SeatDetailsIDId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleModelUser_UsersId",
                table: "RoleModelUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatDetails_ClassId",
                table: "SeatDetails",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatDetails_FlightId",
                table: "SeatDetails",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatDetails_SeatAdditionalServiceId",
                table: "SeatDetails",
                column: "SeatAdditionalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceForClassTravelClass_TravelClassesId",
                table: "ServiceForClassTravelClass",
                column: "TravelClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPrice_ClassID",
                table: "TicketPrice",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPrice_FlightId",
                table: "TicketPrice",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenRemainLogins_UserId",
                table: "TokenRemainLogins",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddionalFoodServiceFlightDetail");

            migrationBuilder.DropTable(
                name: "AddionalFoodServiceReservation");

            migrationBuilder.DropTable(
                name: "FoodForFlight");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ReservationMapAddionalFoodService");

            migrationBuilder.DropTable(
                name: "RoleModelUser");

            migrationBuilder.DropTable(
                name: "ServiceForClassTravelClass");

            migrationBuilder.DropTable(
                name: "TicketPrice");

            migrationBuilder.DropTable(
                name: "TokenRemainLogins");

            migrationBuilder.DropTable(
                name: "AddionalFoodService");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ServiceForClasses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "SeatDetails");

            migrationBuilder.DropTable(
                name: "AdditionalSeatService");

            migrationBuilder.DropTable(
                name: "FlightDetails");

            migrationBuilder.DropTable(
                name: "TravelClasses");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
