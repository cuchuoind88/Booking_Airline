using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Airline.Migrations
{
    /// <inheritdoc />
    public partial class Addcountrytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirportCountry",
                table: "Airports");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Airports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    countryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airports_CountryId",
                table: "Airports",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Airports_Countries_CountryId",
                table: "Airports",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airports_Countries_CountryId",
                table: "Airports");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Airports_CountryId",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Airports");

            migrationBuilder.AddColumn<string>(
                name: "AirportCountry",
                table: "Airports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
