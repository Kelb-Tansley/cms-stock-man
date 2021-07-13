using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Systems.StockManagement.Data.Migrations
{
    public partial class ImageIsPrimary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStocks",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 765, DateTimeKind.Local).AddTicks(4752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(8374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStocks",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 765, DateTimeKind.Local).AddTicks(3963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(7768));

            migrationBuilder.AlterColumn<string>(
                name: "StockImage",
                table: "VehicleStockImages",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockImages",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 766, DateTimeKind.Local).AddTicks(625),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(5043));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockImages",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 766, DateTimeKind.Local).AddTicks(302),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(4515));

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "VehicleStockImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockAccessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 767, DateTimeKind.Local).AddTicks(6514),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9844));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockAccessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 767, DateTimeKind.Local).AddTicks(6192),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 764, DateTimeKind.Local).AddTicks(3156),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 867, DateTimeKind.Local).AddTicks(9767));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accessories",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 762, DateTimeKind.Local).AddTicks(4926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 866, DateTimeKind.Local).AddTicks(3540));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "VehicleStockImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(8374),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 765, DateTimeKind.Local).AddTicks(4752));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 868, DateTimeKind.Local).AddTicks(7768),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 765, DateTimeKind.Local).AddTicks(3963));

            migrationBuilder.AlterColumn<byte[]>(
                name: "StockImage",
                table: "VehicleStockImages",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(5043),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 766, DateTimeKind.Local).AddTicks(625));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 869, DateTimeKind.Local).AddTicks(4515),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 766, DateTimeKind.Local).AddTicks(302));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "VehicleStockAccessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9844),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 767, DateTimeKind.Local).AddTicks(6514));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleStockAccessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 870, DateTimeKind.Local).AddTicks(9469),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 767, DateTimeKind.Local).AddTicks(6192));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Accessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 867, DateTimeKind.Local).AddTicks(9767),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 764, DateTimeKind.Local).AddTicks(3156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Accessories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 1, 53, 866, DateTimeKind.Local).AddTicks(3540),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 13, 19, 34, 56, 762, DateTimeKind.Local).AddTicks(4926));
        }
    }
}
