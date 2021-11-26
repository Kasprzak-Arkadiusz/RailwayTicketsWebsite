using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddNumberOfFreeSeatsToTrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes");

            migrationBuilder.AddColumn<short>(
                name: "NumberOfFreeSeats",
                table: "Trains",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
            
            migrationBuilder.AddForeignKey(
                name: "fk_train_Routes",
                table: "Routes",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_train_Routes",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "NumberOfFreeSeats",
                table: "Trains");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
