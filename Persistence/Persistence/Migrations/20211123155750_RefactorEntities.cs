using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RefactorEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Dropping the foreign keys
            migrationBuilder.DropForeignKey(
                name: "fk_ReturnedTickets_Tickets",
                table: "ReturnedTickets");

            migrationBuilder.DropForeignKey(
                name: "fk_finalStation_Stations",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_startingStation_Stations",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_Seats_Trains",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_Routes",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_Seats",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_Trains",
                table: "Tickets");

            //Dropping the indexes
            migrationBuilder.DropIndex(
                name: "IX_Tickets_route",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_seat",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_train",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Seats_train",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Routes_finalStation",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_startingStation",
                table: "Routes");

            /*migrationBuilder.DropIndex(
                name: "IX_ReturnedTickets_ticket",
                table: "ReturnedTickets");*/

            //Dropping the columns
            migrationBuilder.DropColumn(
                name: "owner",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "route",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "seat",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "train",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "train",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "finalStation",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "startingStation",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ticket",
                table: "ReturnedTickets");

            //Adding the columns
            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ownerId",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainId",
                table: "Seats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalStationId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingStationId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "ReturnedTickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RouteId",
                table: "Tickets",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TrainId",
                table: "Tickets",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TrainId",
                table: "Seats",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_FinalStationId",
                table: "Routes",
                column: "FinalStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StartingStationId",
                table: "Routes",
                column: "StartingStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TrainId",
                table: "Routes",
                column: "TrainId");

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
                name: "fk_finalStation_Stations",
                table: "Routes",
                column: "FinalStationId",
                principalTable: "Stations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_startingStation_Stations",
                table: "Routes",
                column: "StartingStationId",
                principalTable: "Stations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Seats_Trains",
                table: "Seats",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_Routes",
                table: "Tickets",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_Seats",
                table: "Tickets",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_Trains",
                table: "Tickets",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_ReturnedTickets_Tickets",
                table: "ReturnedTickets");

            migrationBuilder.DropForeignKey(
                name: "fk_finalStation_Stations",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_startingStation_Stations",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "fk_Seats_Trains",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_Routes",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_Seats",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "fk_Tickets_Trains",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_RouteId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TrainId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Seats_TrainId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Routes_FinalStationId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_StartingStationId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_TrainId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_ReturnedTickets_TicketId",
                table: "ReturnedTickets");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ownerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "FinalStationId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartingStationId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "ReturnedTickets");

            migrationBuilder.AddColumn<int>(
                name: "owner",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "route",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "seat",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "train",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "train",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "finalStation",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "startingStation",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ticket",
                table: "ReturnedTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Seats_train",
                table: "Seats",
                column: "train");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_finalStation",
                table: "Routes",
                column: "finalStation");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_startingStation",
                table: "Routes",
                column: "startingStation");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedTickets_ticket",
                table: "ReturnedTickets",
                column: "ticket");

            migrationBuilder.AddForeignKey(
                name: "fk_ReturnedTickets_Tickets",
                table: "ReturnedTickets",
                column: "ticket",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_finalStation_Stations",
                table: "Routes",
                column: "finalStation",
                principalTable: "Stations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_startingStation_Stations",
                table: "Routes",
                column: "startingStation",
                principalTable: "Stations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Seats_Trains",
                table: "Seats",
                column: "train",
                principalTable: "Trains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_Routes",
                table: "Tickets",
                column: "route",
                principalTable: "Routes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_Seats",
                table: "Tickets",
                column: "seat",
                principalTable: "Seats",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_Tickets_Trains",
                table: "Tickets",
                column: "train",
                principalTable: "Trains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
