using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Data.Migrations
{
    public partial class ChangeAmountToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Balance",
                table: "Account",
                nullable: false,
                defaultValue: 100000,
                oldClrType: typeof(float),
                oldType: "real",
                oldDefaultValue: 1000f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Balance",
                table: "Account",
                type: "real",
                nullable: false,
                defaultValue: 1000f,
                oldClrType: typeof(int),
                oldDefaultValue: 100000);
        }
    }
}
