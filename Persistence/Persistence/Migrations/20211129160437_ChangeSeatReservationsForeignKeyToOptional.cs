using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangeSeatReservationsForeignKeyToOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatReservations_SeatForeignKey",
                table: "Seats");

            migrationBuilder.AlterColumn<int>(
                name: "SeatForeignKey",
                table: "Seats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatForeignKey",
                table: "Seats",
                column: "SeatForeignKey",
                unique: false,
                filter: "[SeatForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatReservations_SeatForeignKey",
                table: "Seats",
                column: "SeatForeignKey",
                principalTable: "SeatReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_SeatReservations_SeatForeignKey",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_SeatForeignKey",
                table: "Seats");

            migrationBuilder.AlterColumn<int>(
                name: "SeatForeignKey",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatForeignKey",
                table: "Seats",
                column: "SeatForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_SeatReservations_SeatForeignKey",
                table: "Seats",
                column: "SeatForeignKey",
                principalTable: "SeatReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
