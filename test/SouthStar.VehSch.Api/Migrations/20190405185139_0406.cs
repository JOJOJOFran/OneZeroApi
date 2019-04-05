using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SouthStar.VehSch.Api.Migrations
{
    public partial class _0406 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_TVehicles_VIN_IsDelete_TenanId",
            //    table: "TVehicles");

            //migrationBuilder.DropIndex(
            //    name: "IX_TDrivers_PhoneNum_IsDelete_TenanId",
            //    table: "TDrivers");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "TVehicles",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "TankCapacity",
                table: "TVehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "PurchasePrice",
                table: "TVehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "InitMileage",
                table: "TVehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedSeating",
                table: "TVehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UseArea",
                table: "TVehicleDispatchs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "TUser",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateIndex(
            //    name: "IX_TVehicles_VIN_IsDelete_TenanId",
            //    table: "TVehicles",
            //    columns: new[] { "VIN", "IsDelete", "TenanId" },
            //    unique: true,
            //    filter: "[VIN] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TDrivers_MobileNum_IsDelete_TenanId",
            //    table: "TDrivers",
            //    columns: new[] { "MobileNum", "IsDelete", "TenanId" },
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_TVehicles_VIN_IsDelete_TenanId",
            //    table: "TVehicles");

            //migrationBuilder.DropIndex(
            //    name: "IX_TDrivers_MobileNum_IsDelete_TenanId",
            //    table: "TDrivers");

            migrationBuilder.DropColumn(
                name: "UseArea",
                table: "TVehicleDispatchs");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "TUser");

            migrationBuilder.AlterColumn<string>(
                name: "VIN",
                table: "TVehicles",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TankCapacity",
                table: "TVehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PurchasePrice",
                table: "TVehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InitMileage",
                table: "TVehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedSeating",
                table: "TVehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TVehicles_VIN_IsDelete_TenanId",
                table: "TVehicles",
                columns: new[] { "VIN", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TDrivers_PhoneNum_IsDelete_TenanId",
                table: "TDrivers",
                columns: new[] { "PhoneNum", "IsDelete", "TenanId" },
                unique: true,
                filter: "[PhoneNum] IS NOT NULL");
        }
    }
}
