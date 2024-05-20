using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace post_office_back.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DestinationAirport = table.Column<int>(type: "int", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentNumber);
                });

            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    BagNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShipmentNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.BagNumber);
                    table.ForeignKey(
                        name: "FK_Bags_Shipments_ShipmentNumber",
                        column: x => x.ShipmentNumber,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bags_ShipmentNumber",
                table: "Bags",
                column: "ShipmentNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bags");

            migrationBuilder.DropTable(
                name: "Shipments");
        }
    }
}
