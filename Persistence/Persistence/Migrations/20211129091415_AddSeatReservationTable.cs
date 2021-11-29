using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddSeatReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Routes_arrivalTimeInMinutesPastMidnight",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_departureTimeInMinutesPastMidnight",
                table: "Routes");


            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_Seats",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "NumberOfFreeSeats",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "dayOfDeparture",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "isFree",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "arrivalTimeInMinutesPastMidnight",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "isOnHold",
                table: "Routes",
                newName: "isSuspended");

            migrationBuilder.RenameColumn(
                name: "departureTimeInMinutesPastMidnight",
                table: "Routes",
                newName: "NumberOfFreeSeats");

            migrationBuilder.AddColumn<int>(
                name: "SeatForeignKey",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "arrivalTime",
                table: "Routes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "departureTime",
                table: "Routes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SeatReservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Traindeparturetime = table.Column<DateTime>(name: "Train departure time", type: "datetime2", nullable: false),
                    SeatReservationForeignKey = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatReservations", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Tickets_SeatReservations",
                        column: x => x.SeatReservationForeignKey,
                        principalTable: "Tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatReservations_SeatReservationForeignKey",
                table: "SeatReservations",
                column: "SeatReservationForeignKey",
                unique: true,
                filter: "[SeatReservationForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatReservations_SeatForeignKey",
                table: "Seats",
                column: "SeatForeignKey",
                principalTable: "SeatReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatReservations_SeatForeignKey",
                table: "Seats");

            migrationBuilder.DropTable(
                name: "SeatReservations");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeatForeignKey",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "SeatForeignKey",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "arrivalTime",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "departureTime",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "isSuspended",
                table: "Routes",
                newName: "isOnHold");

            migrationBuilder.RenameColumn(
                name: "NumberOfFreeSeats",
                table: "Routes",
                newName: "departureTimeInMinutesPastMidnight");

            migrationBuilder.AddColumn<short>(
                name: "NumberOfFreeSeats",
                table: "Trains",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dayOfDeparture",
                table: "Tickets",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isFree",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<short>(
                name: "arrivalTimeInMinutesPastMidnight",
                table: "Routes",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_Seats",
                table: "Tickets",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
