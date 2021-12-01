using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemoveOneToOneFromTicketsSeatReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_SeatReservations",
                table: "SeatReservations");

            migrationBuilder.DropColumn(
                name: "SeatForeignKey",
                table: "Seats");


            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "SeatReservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatReservations_TicketId",
                table: "SeatReservations",
                column: "TicketId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatReservations_Seats_SeatReservationForeignKey",
                table: "SeatReservations");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_SeatReservations",
                table: "SeatReservations");

            migrationBuilder.DropIndex(
                name: "IX_SeatReservations_TicketId",
                table: "SeatReservations");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "SeatReservations");

            migrationBuilder.AddColumn<int>(
                name: "SeatForeignKey",
                table: "Seats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Train departure time",
                table: "SeatReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatForeignKey",
                table: "Seats",
                column: "SeatForeignKey",
                unique: true,
                filter: "[SeatForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_SeatReservations",
                table: "SeatReservations",
                column: "SeatReservationForeignKey",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatReservations_SeatForeignKey",
                table: "Seats",
                column: "SeatForeignKey",
                principalTable: "SeatReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
