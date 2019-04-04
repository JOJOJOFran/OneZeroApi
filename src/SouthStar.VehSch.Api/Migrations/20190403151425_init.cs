﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SouthStar.VehSch.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TCheckContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    ApplyNum = table.Column<string>(maxLength: 50, nullable: true),
                    ApplyId = table.Column<Guid>(nullable: false),
                    CheckUserId = table.Column<Guid>(nullable: false),
                    CheckUserName = table.Column<string>(maxLength: 30, nullable: true),
                    ApplyType = table.Column<int>(nullable: false),
                    CheckStatus = table.Column<int>(nullable: false),
                    CheckReply = table.Column<string>(maxLength: 500, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCheckContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    ParentDepartmentId = table.Column<Guid>(nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 100, nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    RowNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TDispatchFees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    DispatchId = table.Column<Guid>(nullable: false),
                    EndMiles = table.Column<float>(nullable: false),
                    StartMiles = table.Column<float>(nullable: false),
                    UnitPrice = table.Column<float>(nullable: false),
                    HighSpeedFee = table.Column<float>(nullable: false),
                    EtcFee = table.Column<float>(nullable: false),
                    ParkFee = table.Column<float>(nullable: false),
                    OilFee = table.Column<float>(nullable: false),
                    TotalPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDispatchFees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TDrivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    PhoneNum = table.Column<string>(maxLength: 20, nullable: true),
                    MobileNum = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    DrivingLicNum = table.Column<string>(maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    PermittedType = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<int>(maxLength: 10, nullable: false),
                    Image = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDrivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TModuleType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TModuleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TPermissionType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    ModuleId = table.Column<Guid>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 50, nullable: true),
                    ApiPath = table.Column<string>(maxLength: 50, nullable: true),
                    RowNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPermissionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: false),
                    Remark = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    Account = table.Column<string>(maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 2000, nullable: false),
                    Email = table.Column<string>(maxLength: 2000, nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVehicleApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    ApplyNum = table.Column<string>(maxLength: 50, nullable: false),
                    ApplicantId = table.Column<Guid>(nullable: false),
                    ApplicantName = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicantPhone = table.Column<string>(maxLength: 30, nullable: true),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 500, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    UserMobile = table.Column<string>(maxLength: 50, nullable: true),
                    UserTitle = table.Column<string>(maxLength: 50, nullable: true),
                    UserCount = table.Column<string>(maxLength: 10, nullable: false),
                    CarType = table.Column<string>(maxLength: 50, nullable: false),
                    CarProperty = table.Column<int>(nullable: false),
                    UseArea = table.Column<string>(maxLength: 100, nullable: true),
                    StartPoint = table.Column<string>(maxLength: 200, nullable: true),
                    Destination = table.Column<string>(maxLength: 200, nullable: true),
                    ApplyReson = table.Column<string>(maxLength: 500, nullable: true),
                    StartPlanTime = table.Column<DateTime>(nullable: false),
                    BackPlanTime = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(maxLength: 2000, nullable: true),
                    ReciverId = table.Column<Guid>(nullable: false),
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
                name: "TVehicleDispatchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    ApplyId = table.Column<Guid>(nullable: false),
                    ApplyNum = table.Column<string>(maxLength: 100, nullable: true),
                    ApplicantName = table.Column<string>(nullable: true),
                    ApplicantPhone = table.Column<string>(nullable: true),
                    ApplyDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    UserCount = table.Column<string>(nullable: true),
                    UserTitle = table.Column<string>(nullable: true),
                    UserMobile = table.Column<string>(nullable: true),
                    UserDepartment = table.Column<string>(nullable: true),
                    StartPoint = table.Column<string>(maxLength: 200, nullable: true),
                    Destination = table.Column<string>(maxLength: 200, nullable: true),
                    ApplyReson = table.Column<string>(maxLength: 500, nullable: true),
                    StartPlanTime = table.Column<DateTime>(nullable: false),
                    BackPlanTime = table.Column<DateTime>(nullable: false),
                    CarType = table.Column<string>(maxLength: 50, nullable: true),
                    CarProperty = table.Column<int>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false),
                    DriverName = table.Column<string>(nullable: true),
                    DriverPhone = table.Column<string>(nullable: true),
                    VehcileId = table.Column<Guid>(nullable: false),
                    PlateNumber = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DispatchType = table.Column<int>(nullable: false),
                    QueueNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVehicleDispatchs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
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
                    VehicleProperties = table.Column<int>(nullable: false),
                    FACardNum = table.Column<string>(maxLength: 20, nullable: true),
                    OilType = table.Column<int>(maxLength: 20, nullable: false),
                    CarIcon = table.Column<string>(maxLength: 100, nullable: true),
                    LoadWeight = table.Column<string>(maxLength: 20, nullable: true),
                    CurbWeight = table.Column<string>(maxLength: 20, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    CurrentState = table.Column<int>(maxLength: 50, nullable: false),
                    Image = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRoleModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ModuleId = table.Column<Guid>(nullable: false),
                    RowNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRoleModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRoleModule_TRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRolePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ModuleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false),
                    SeqNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRolePermission_TRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TUserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    RowNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TUserRole_TUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TCheckContents_ApplyNum_IsDelete_TenanId",
                table: "TCheckContents",
                columns: new[] { "ApplyNum", "IsDelete", "TenanId" },
                unique: true,
                filter: "[ApplyNum] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TDepartments_DepartmentName_TenanId_IsDelete",
                table: "TDepartments",
                columns: new[] { "DepartmentName", "TenanId", "IsDelete" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TDispatchFees_DispatchId_TenanId_IsDelete",
                table: "TDispatchFees",
                columns: new[] { "DispatchId", "TenanId", "IsDelete" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TDrivers_DrivingLicNum_IsDelete_TenanId",
                table: "TDrivers",
                columns: new[] { "DrivingLicNum", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TDrivers_PhoneNum_IsDelete_TenanId",
                table: "TDrivers",
                columns: new[] { "PhoneNum", "IsDelete", "TenanId" },
                unique: true,
                filter: "[PhoneNum] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TModuleType_Name_IsDelete_TenanId",
                table: "TModuleType",
                columns: new[] { "Name", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TPermissionType_Name_IsDelete_TenanId",
                table: "TPermissionType",
                columns: new[] { "Name", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TRole_Name_IsDelete_TenanId",
                table: "TRole",
                columns: new[] { "Name", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TRoleModule_RoleId_ModuleId_TenanId",
                table: "TRoleModule",
                columns: new[] { "RoleId", "ModuleId", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TRolePermission_RoleId",
                table: "TRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TRolePermission_SeqNo",
                table: "TRolePermission",
                column: "SeqNo");

            migrationBuilder.CreateIndex(
                name: "IX_TRolePermission_PermissionId_RoleId_TenanId",
                table: "TRolePermission",
                columns: new[] { "PermissionId", "RoleId", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TUser_Account_IsDelete_TenanId",
                table: "TUser",
                columns: new[] { "Account", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TUserRole_UserId",
                table: "TUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TUserRole_RoleId_UserId_TenanId",
                table: "TUserRole",
                columns: new[] { "RoleId", "UserId", "TenanId" });

            migrationBuilder.CreateIndex(
                name: "IX_TVehicleApplications_ApplyNum_IsDelete_TenanId",
                table: "TVehicleApplications",
                columns: new[] { "ApplyNum", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TVehicleDispatchs_ApplyNum_TenanId_IsDelete",
                table: "TVehicleDispatchs",
                columns: new[] { "ApplyNum", "TenanId", "IsDelete" },
                unique: true,
                filter: "[ApplyNum] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TVehicles_EngineNo_IsDelete_TenanId",
                table: "TVehicles",
                columns: new[] { "EngineNo", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TVehicles_PlateNumber_IsDelete_TenanId",
                table: "TVehicles",
                columns: new[] { "PlateNumber", "IsDelete", "TenanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TVehicles_VIN_IsDelete_TenanId",
                table: "TVehicles",
                columns: new[] { "VIN", "IsDelete", "TenanId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCheckContents");

            migrationBuilder.DropTable(
                name: "TDepartments");

            migrationBuilder.DropTable(
                name: "TDispatchFees");

            migrationBuilder.DropTable(
                name: "TDrivers");

            migrationBuilder.DropTable(
                name: "TModuleType");

            migrationBuilder.DropTable(
                name: "TPermissionType");

            migrationBuilder.DropTable(
                name: "TRoleModule");

            migrationBuilder.DropTable(
                name: "TRolePermission");

            migrationBuilder.DropTable(
                name: "TUserRole");

            migrationBuilder.DropTable(
                name: "TVehicleApplications");

            migrationBuilder.DropTable(
                name: "TVehicleDispatchs");

            migrationBuilder.DropTable(
                name: "TVehicles");

            migrationBuilder.DropTable(
                name: "TRole");

            migrationBuilder.DropTable(
                name: "TUser");
        }
    }
}