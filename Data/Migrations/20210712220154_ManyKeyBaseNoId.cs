using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Systems.StockManagement.Data.Migrations
{
    public partial class ManyKeyBaseNoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_VehicleStocks_VehicleStockId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleStockImages_VehicleStocks_VehicleStockId",
                table: "VehicleStockImages");

            migrationBuilder.DropIndex(
                name: "IX_VehicleStockImages_VehicleStockId",
                table: "VehicleStockImages");

            migrationBuilder.DropIndex(
                name: "IX_Accessories_VehicleStockId",
                table: "Accessories");

            migrationBuilder.DropColumn(
                name: "VehicleStockAccessoryId",
                table: "VehicleStockAccessories");

            migrationBuilder.DropColumn(
                name: "VehicleStockId",
                table: "Accessories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStocks",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(8374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(176));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStocks",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(7768),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 75, DateTimeKind.Local).AddTicks(9870));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockImages",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(5043),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(3737));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockImages",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(4515),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(3384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockAccessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9844),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 77, DateTimeKind.Local).AddTicks(5712));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockAccessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9469),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 77, DateTimeKind.Local).AddTicks(5405));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 867, DateTimeKind.Local).AddTicks(9767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 75, DateTimeKind.Local).AddTicks(5874));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 866, DateTimeKind.Local).AddTicks(3540),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 74, DateTimeKind.Local).AddTicks(2712));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(176),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(8374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 75, DateTimeKind.Local).AddTicks(9870),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(7768));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(3737),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(5043));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 76, DateTimeKind.Local).AddTicks(3384),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(4515));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockAccessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 77, DateTimeKind.Local).AddTicks(5712),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9844));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockAccessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 77, DateTimeKind.Local).AddTicks(5405),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.AddColumn<int>(
                name: "VehicleStockAccessoryId",
                table: "VehicleStockAccessories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 75, DateTimeKind.Local).AddTicks(5874),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 867, DateTimeKind.Local).AddTicks(9767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 12, 21, 32, 9, 74, DateTimeKind.Local).AddTicks(2712),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 866, DateTimeKind.Local).AddTicks(3540));

            migrationBuilder.AddColumn<int>(
                name: "VehicleStockId",
                table: "Accessories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleStockImages_VehicleStockId",
                table: "VehicleStockImages",
                column: "VehicleStockId");

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_VehicleStockId",
                table: "Accessories",
                column: "VehicleStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_VehicleStocks_VehicleStockId",
                table: "Accessories",
                column: "VehicleStockId",
                principalTable: "VehicleStocks",
                principalColumn: "VehicleStockId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleStockImages_VehicleStocks_VehicleStockId",
                table: "VehicleStockImages",
                column: "VehicleStockId",
                principalTable: "VehicleStocks",
                principalColumn: "VehicleStockId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
