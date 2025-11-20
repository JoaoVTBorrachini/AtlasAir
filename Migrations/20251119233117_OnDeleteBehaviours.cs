using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtlasAir.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteBehaviours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "WaitingPayment");

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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "WaitingPayment",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Pending");

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
    }
}
