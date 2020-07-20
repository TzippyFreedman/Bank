using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Data.Migrations
{
    public partial class AddedVerificationCodev_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationTime",
                table: "EmailVerification",
                nullable: false,
                defaultValueSql: "dateadd(minute,5,getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "dateadd(minute,2,getdate())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationTime",
                table: "EmailVerification",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "dateadd(minute,2,getdate())",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "dateadd(minute,5,getdate())");
        }
    }
}
