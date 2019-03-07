using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SouthStar.VehSch.Api.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "TVehicles",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenanId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    ParentDepartmentId = table.Column<int>(nullable: false),
                    DepartmentName = table.Column<string>(maxLength: 100, nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDepartments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TDepartments_DepartmentName",
                table: "TDepartments",
                column: "DepartmentName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TDepartments");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentState",
                table: "TVehicles",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 50);
        }
    }
}
