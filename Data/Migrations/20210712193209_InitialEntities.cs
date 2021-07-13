using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Systems.StockManagement.Data.Migrations
{
    public partial class InitialEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleStockAccessories",
                columns: table => new
                {
                    VehicleStockId = table.Column<int>(nullable: false),
                    AccessoryId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 77, DateTimeKind.Local).AddTicks(5405)),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 77, DateTimeKind.Local).AddTicks(5712)),
                    VehicleStockAccessoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleStockAccessories", x => new { x.AccessoryId, x.VehicleStockId });
                });

            migrationBuilder.CreateTable(
                name: "VehicleStocks",
                columns: table => new
                {
                    VehicleStockId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 75, DateTimeKind.Local).AddTicks(9870)),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(176)),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    VinNumber = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    ModelDescription = table.Column<string>(nullable: true),
                    ModelYear = table.Column<int>(nullable: false),
                    CurrentKilometreReading = table.Column<double>(nullable: false),
                    Colour = table.Column<string>(nullable: true),
                    RetailPrice = table.Column<double>(nullable: false),
                    CostPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleStocks", x => x.VehicleStockId);
                });

            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    AccessoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 74, DateTimeKind.Local).AddTicks(2712)),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 75, DateTimeKind.Local).AddTicks(5874)),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    VehicleStockId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.AccessoryId);
                    table.ForeignKey(
                        name: "FK_Accessories_VehicleStocks_VehicleStockId",
                        column: x => x.VehicleStockId,
                        principalTable: "VehicleStocks",
                        principalColumn: "VehicleStockId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleStockImages",
                columns: table => new
                {
                    VehicleStockImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(3384)),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(3737)),
                    VehicleStockId = table.Column<int>(nullable: false),
                    StockImage = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleStockImages", x => x.VehicleStockImageId);
                    table.ForeignKey(
                        name: "FK_VehicleStockImages_VehicleStocks_VehicleStockId",
                        column: x => x.VehicleStockId,
                        principalTable: "VehicleStocks",
                        principalColumn: "VehicleStockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_VehicleStockId",
                table: "Accessories",
                column: "VehicleStockId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleStockImages_VehicleStockId",
                table: "VehicleStockImages",
                column: "VehicleStockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "VehicleStockAccessories");

            migrationBuilder.DropTable(
                name: "VehicleStockImages");

            migrationBuilder.DropTable(
                name: "VehicleStocks");
        }
    }
}
