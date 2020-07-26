using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransferService.Data.Migrations
{
    public partial class Changed_Names_Of_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAccount",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ToAccount",
                table: "Transfer");

            migrationBuilder.AddColumn<Guid>(
                name: "DestAccount",
                table: "Transfer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SrcAccount",
                table: "Transfer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestAccount",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SrcAccount",
                table: "Transfer");

            migrationBuilder.AddColumn<Guid>(
                name: "FromAccount",
                table: "Transfer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ToAccount",
                table: "Transfer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
