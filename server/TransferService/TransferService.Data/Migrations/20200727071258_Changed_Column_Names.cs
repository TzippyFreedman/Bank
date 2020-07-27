using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransferService.Data.Migrations
{
    public partial class Changed_Column_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestAccount",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SrcAccount",
                table: "Transfer");

            migrationBuilder.AddColumn<Guid>(
                name: "DestAccountId",
                table: "Transfer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SrcAccountId",
                table: "Transfer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestAccountId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SrcAccountId",
                table: "Transfer");

            migrationBuilder.AddColumn<Guid>(
                name: "DestAccount",
                table: "Transfer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SrcAccount",
                table: "Transfer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
