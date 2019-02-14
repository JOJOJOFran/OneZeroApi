using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SouthStar.VehSch.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TModulePermission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    ModuleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false),
                    SeqNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TModulePermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
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
                name: "TUserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    RowNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 256, nullable: false),
                    Remark = table.Column<string>(maxLength: 2000, nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TRole_TUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TModuleType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    ModuleTypeId = table.Column<Guid>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TModuleType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TModuleType_TModuleType_ModuleTypeId",
                        column: x => x.ModuleTypeId,
                        principalTable: "TModuleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TModuleType_TRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TPermissionType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 50, nullable: true),
                    ApiPath = table.Column<string>(maxLength: 50, nullable: true),
                    RowNo = table.Column<int>(nullable: false),
                    ModuleTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPermissionType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TPermissionType_TModuleType_ModuleTypeId",
                        column: x => x.ModuleTypeId,
                        principalTable: "TModuleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TModulePermission_SeqNo",
                table: "TModulePermission",
                column: "SeqNo");

            migrationBuilder.CreateIndex(
                name: "IX_TModulePermission_PermissionId_ModuleId",
                table: "TModulePermission",
                columns: new[] { "PermissionId", "ModuleId" });

            migrationBuilder.CreateIndex(
                name: "IX_TModuleType_ModuleTypeId",
                table: "TModuleType",
                column: "ModuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TModuleType_Name",
                table: "TModuleType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TModuleType_RoleId",
                table: "TModuleType",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TPermissionType_ModuleTypeId",
                table: "TPermissionType",
                column: "ModuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TPermissionType_Name",
                table: "TPermissionType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TRole_Name",
                table: "TRole",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TRole_UserId",
                table: "TRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TUser_UserName",
                table: "TUser",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TUserRole_RoleId_UserId",
                table: "TUserRole",
                columns: new[] { "RoleId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TModulePermission");

            migrationBuilder.DropTable(
                name: "TPermissionType");

            migrationBuilder.DropTable(
                name: "TUserRole");

            migrationBuilder.DropTable(
                name: "TModuleType");

            migrationBuilder.DropTable(
                name: "TRole");

            migrationBuilder.DropTable(
                name: "TUser");
        }
    }
}
