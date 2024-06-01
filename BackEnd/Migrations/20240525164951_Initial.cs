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
                    DestinationAirport = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    ShipmentNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CountOfLetters = table.Column<long>(type: "bigint", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.BagNumber);
                    table.ForeignKey(
                        name: "FK_Bags_Shipments_ShipmentNumber",
                        column: x => x.ShipmentNumber,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    ParcelNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ParcelBagBagNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.ParcelNumber);
                    table.ForeignKey(
                        name: "FK_Parcels_Bags_ParcelBagBagNumber",
                        column: x => x.ParcelBagBagNumber,
                        principalTable: "Bags",
                        principalColumn: "BagNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bags_ShipmentNumber",
                table: "Bags",
                column: "ShipmentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelBagBagNumber",
                table: "Parcels",
                column: "ParcelBagBagNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Bags");

            migrationBuilder.DropTable(
                name: "Shipments");
        }
    }
}
