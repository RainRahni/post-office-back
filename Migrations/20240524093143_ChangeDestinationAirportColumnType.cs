using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace post_office_back.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDestinationAirportColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DestinationAirport",
                table: "Shipments",
                type: "nvarchar(3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DestinationAirport",
                table: "Shipments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
