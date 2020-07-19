using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Data.Migrations
{
    public partial class AddPasswordSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "User",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "User",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "User",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
