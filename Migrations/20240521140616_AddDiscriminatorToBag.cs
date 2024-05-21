using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace post_office_back.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscriminatorToBag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountOfLetters",
                table: "Bags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Bags",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Bags",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Bags",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    ParcelNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParcelBagBagNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.ParcelNumber);
                    table.ForeignKey(
                        name: "FK_Parcels_Bags_ParcelBagBagNumber",
                        column: x => x.ParcelBagBagNumber,
                        principalTable: "Bags",
                        principalColumn: "BagNumber");
                });

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

            migrationBuilder.DropColumn(
                name: "CountOfLetters",
                table: "Bags");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Bags");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Bags");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Bags");
        }
    }
}
