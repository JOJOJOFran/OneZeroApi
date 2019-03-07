using Microsoft.EntityFrameworkCore.Migrations;

namespace SouthStar.VehSch.Api.Migrations
{
    public partial class init0305 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OilType",
                table: "TVehicles",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OilType",
                table: "TVehicles",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 20);
        }
    }
}
