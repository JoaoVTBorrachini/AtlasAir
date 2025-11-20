using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtlasAir.Migrations
{
    /// <inheritdoc />
    public partial class FlightFlightSegmentsRelationshipFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightSegment_Flight_FlightId1",
                table: "FlightSegment");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Flight_FlightId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_FlightId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_FlightSegment_FlightId1",
                table: "FlightSegment");

            migrationBuilder.DropColumn(
                name: "FlightId1",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "FlightId1",
                table: "FlightSegment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId1",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightId1",
                table: "FlightSegment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_FlightId1",
                table: "Reservation",
                column: "FlightId1");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSegment_FlightId1",
                table: "FlightSegment",
                column: "FlightId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightSegment_Flight_FlightId1",
                table: "FlightSegment",
                column: "FlightId1",
                principalTable: "Flight",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Flight_FlightId1",
                table: "Reservation",
                column: "FlightId1",
                principalTable: "Flight",
                principalColumn: "Id");
        }
    }
}
