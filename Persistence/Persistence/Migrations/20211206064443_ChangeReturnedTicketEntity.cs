using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangeReturnedTicketEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_ReturnedTickets_Tickets",
                table: "ReturnedTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservations_Seats_SeatReservationForeignKey",
                table: "SeatReservations");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_SeatReservations",
                table: "SeatReservations");

            migrationBuilder.DropIndex(
                name: "IX_ReturnedTickets_TicketId",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "ReturnedTickets");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "ReturnedTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte>(
                name: "Car",
                table: "ReturnedTickets",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "ReturnedTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FinalStationName",
                table: "ReturnedTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Number",
                table: "ReturnedTickets",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "OwnerEmail",
                table: "ReturnedTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartingStationName",
                table: "ReturnedTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "TrainId",
                table: "ReturnedTickets",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservations_Seats_SeatReservationForeignKey",
                table: "SeatReservations",
                column: "SeatReservationForeignKey",
                principalTable: "Seats",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_SeatReservations",
                table: "SeatReservations",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservations_Seats_SeatReservationForeignKey",
                table: "SeatReservations");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_SeatReservations",
                table: "SeatReservations");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "Car",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "FinalStationName",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "OwnerEmail",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "StartingStationName",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "ReturnedTickets");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "ReturnedTickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedTickets_TicketId",
                table: "ReturnedTickets",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "fk_ReturnedTickets_Tickets",
                table: "ReturnedTickets",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatReservations_Seats_SeatReservationForeignKey",
                table: "SeatReservations",
                column: "SeatReservationForeignKey",
                principalTable: "Seats",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_SeatReservations",
                table: "SeatReservations",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
