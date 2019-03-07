using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SouthStar.VehSch.Api.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TDrivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Sex = table.Column<string>(maxLength: 10, nullable: true),
                    Age = table.Column<int>(nullable: false),
                    PhoneNum = table.Column<string>(maxLength: 20, nullable: true),
                    MobileNum = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    DrivingLicNum = table.Column<string>(maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    PermittedType = table.Column<string>(maxLength: 30, nullable: true),
                    DrivingLicType = table.Column<string>(maxLength: 30, nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<string>(maxLength: 10, nullable: true),
                    Image = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDrivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVehicleApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    ApplyNum = table.Column<string>(maxLength: 50, nullable: false),
                    ApplicantId = table.Column<int>(nullable: false),
                    ApplicantName = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicantPhone = table.Column<string>(maxLength: 30, nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 500, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    UserMobile = table.Column<string>(maxLength: 50, nullable: true),
                    UserTitle = table.Column<string>(maxLength: 50, nullable: true),
                    UserCount = table.Column<string>(maxLength: 10, nullable: false),
                    CarType = table.Column<string>(maxLength: 50, nullable: false),
                    CarProperty = table.Column<string>(maxLength: 50, nullable: false),
                    UseArea = table.Column<string>(maxLength: 100, nullable: true),
                    StartPoint = table.Column<string>(maxLength: 200, nullable: true),
                    Destination = table.Column<string>(maxLength: 200, nullable: true),
                    ApplyReson = table.Column<string>(maxLength: 500, nullable: true),
                    StartPlanTime = table.Column<DateTime>(nullable: false),
                    BackPlanTime = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(maxLength: 100, nullable: true),
                    ReciverId = table.Column<int>(nullable: true),
                    ReciverMobile = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVehicleApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    PlateNumber = table.Column<string>(maxLength: 20, nullable: false),
                    VehicleColor = table.Column<string>(maxLength: 20, nullable: true),
                    VehicleBrand = table.Column<string>(maxLength: 50, nullable: true),
                    VechileType = table.Column<string>(maxLength: 50, nullable: false),
                    ApprovedSeating = table.Column<int>(nullable: false),
                    VIN = table.Column<string>(maxLength: 20, nullable: false),
                    EngineNo = table.Column<string>(maxLength: 20, nullable: false),
                    PurchasePrice = table.Column<int>(nullable: false),
                    TerminalNo = table.Column<string>(maxLength: 30, nullable: true),
                    InitMileage = table.Column<int>(nullable: false),
                    TankCapacity = table.Column<int>(nullable: false),
                    VehicleLicOwner = table.Column<string>(maxLength: 30, nullable: true),
                    BookOriginValue = table.Column<string>(maxLength: 20, nullable: true),
                    Displacament = table.Column<string>(maxLength: 20, nullable: true),
                    VehicleProperties = table.Column<string>(maxLength: 20, nullable: false),
                    FACardNum = table.Column<string>(maxLength: 20, nullable: true),
                    OilType = table.Column<string>(maxLength: 20, nullable: true),
                    CarIcon = table.Column<string>(maxLength: 100, nullable: true),
                    LoadWeight = table.Column<string>(maxLength: 20, nullable: true),
                    CurbWeight = table.Column<string>(maxLength: 20, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    CurrentState = table.Column<string>(maxLength: 50, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVehicles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TDrivers_DrivingLicNum",
                table: "TDrivers",
                column: "DrivingLicNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TDrivers_PhoneNum",
                table: "TDrivers",
                column: "PhoneNum",
                unique: true,
                filter: "[PhoneNum] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TVehicleApplications_ApplyNum",
                table: "TVehicleApplications",
                column: "ApplyNum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TVehicles_EngineNo",
                table: "TVehicles",
                column: "EngineNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TVehicles_PlateNumber",
                table: "TVehicles",
                column: "PlateNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TDrivers");

            migrationBuilder.DropTable(
                name: "TVehicleApplications");

            migrationBuilder.DropTable(
                name: "TVehicles");
        }
    }
}
