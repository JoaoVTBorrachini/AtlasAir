using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtlasAir.Migrations
{
    /// <inheritdoc />
    public partial class AircraftFlightSegmentsConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Aircraft_AircraftId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_DestinationAirportId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_OriginAirportId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSegment_Airport_DestinationAirportId",
                table: "FlightSegment");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSegment_Airport_OriginAirportId",
                table: "FlightSegment");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Seat_SeatId1",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Aircraft_AircraftId1",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_AircraftId1",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_SeatId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Flight_AircraftId",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "AircraftId1",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "SeatId1",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "AircraftId",
                table: "Flight");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_DestinationAirportId",
                table: "Flight",
                column: "DestinationAirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_OriginAirportId",
                table: "Flight",
                column: "OriginAirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSegment_Airport_DestinationAirportId",
                table: "FlightSegment",
                column: "DestinationAirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSegment_Airport_OriginAirportId",
                table: "FlightSegment",
                column: "OriginAirportId",
                principalTable: "Airport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_DestinationAirportId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airport_OriginAirportId",
                table: "Flight");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSegment_Airport_DestinationAirportId",
                table: "FlightSegment");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightSegment_Airport_OriginAirportId",
                table: "FlightSegment");

            migrationBuilder.AddColumn<int>(
                name: "AircraftId1",
                table: "Seat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatId1",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AircraftId",
                table: "Flight",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_AircraftId1",
                table: "Seat",
                column: "AircraftId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_SeatId1",
                table: "Reservation",
                column: "SeatId1");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AircraftId",
                table: "Flight",
                column: "AircraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Aircraft_AircraftId",
                table: "Flight",
                column: "AircraftId",
                principalTable: "Aircraft",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_DestinationAirportId",
                table: "Flight",
                column: "DestinationAirportId",
                principalTable: "Airport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airport_OriginAirportId",
                table: "Flight",
                column: "OriginAirportId",
                principalTable: "Airport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSegment_Airport_DestinationAirportId",
                table: "FlightSegment",
                column: "DestinationAirportId",
                principalTable: "Airport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSegment_Airport_OriginAirportId",
                table: "FlightSegment",
                column: "OriginAirportId",
                principalTable: "Airport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Seat_SeatId1",
                table: "Reservation",
                column: "SeatId1",
                principalTable: "Seat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Aircraft_AircraftId1",
                table: "Seat",
                column: "AircraftId1",
                principalTable: "Aircraft",
                principalColumn: "Id");
        }
    }
}
