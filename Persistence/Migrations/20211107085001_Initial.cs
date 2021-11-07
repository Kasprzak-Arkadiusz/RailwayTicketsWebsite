using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trainId = table.Column<short>(type: "smallint", nullable: false),
                    numberOfCars = table.Column<byte>(type: "tinyint", nullable: false),
                    numberOfSeats = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startingStation = table.Column<int>(type: "int", nullable: false),
                    finalStation = table.Column<int>(type: "int", nullable: false),
                    departureTimeInMinutesPastMidnight = table.Column<short>(type: "smallint", nullable: false),
                    arrivalTimeInMinutesPastMidnight = table.Column<short>(type: "smallint", nullable: false),
                    isOnHold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.id);
                    table.ForeignKey(
                        name: "fk_finalStation_Stations",
                        column: x => x.finalStation,
                        principalTable: "Stations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_startingStation_Stations",
                        column: x => x.startingStation,
                        principalTable: "Stations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    train = table.Column<int>(type: "int", nullable: false),
                    car = table.Column<byte>(type: "tinyint", nullable: false),
                    number = table.Column<byte>(type: "tinyint", nullable: false),
                    isFree = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.id);
                    table.ForeignKey(
                        name: "fk_Seats_Trains",
                        column: x => x.train,
                        principalTable: "Trains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    owner = table.Column<int>(type: "int", nullable: false),
                    route = table.Column<int>(type: "int", nullable: false),
                    dayOfDeparture = table.Column<DateTime>(type: "date", nullable: false),
                    seat = table.Column<int>(type: "int", nullable: false),
                    train = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "fk_Tickets_Routes",
                        column: x => x.route,
                        principalTable: "Routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Tickets_Seats",
                        column: x => x.seat,
                        principalTable: "Seats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_Tickets_Trains",
                        column: x => x.train,
                        principalTable: "Trains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReturnedTickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticket = table.Column<int>(type: "int", nullable: false),
                    dateOfReturn = table.Column<DateTime>(type: "date", nullable: false),
                    genericReasonOfReturn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    personalReasonOfReturn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedTickets", x => x.id);
                    table.ForeignKey(
                        name: "fk_ReturnedTickets_Tickets",
                        column: x => x.ticket,
                        principalTable: "Tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            /* Indexes shouldnt be created on table with often changing rows
             migrationBuilder.CreateIndex(
                name: "IX_ReturnedTickets_ticket",
                table: "ReturnedTickets",
                column: "ticket");*/

            migrationBuilder.CreateIndex(
                name: "IX_Routes_finalStation",
                table: "Routes",
                column: "finalStation");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_startingStation",
                table: "Routes",
                column: "startingStation");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_departureTimeInMinutesPastMidnight",
                table: "Routes",
                column: "departureTimeInMinutesPastMidnight");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_arrivalTimeInMinutesPastMidnight",
                table: "Routes",
                column: "arrivalTimeInMinutesPastMidnight");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_train",
                table: "Seats",
                column: "train");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_name",
                table: "Stations",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_route",
                table: "Tickets",
                column: "route");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_seat",
                table: "Tickets",
                column: "seat");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_train",
                table: "Tickets",
                column: "train");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnedTickets");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
