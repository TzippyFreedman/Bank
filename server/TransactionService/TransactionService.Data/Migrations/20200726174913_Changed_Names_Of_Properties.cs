using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionService.Data.Migrations
{
    public partial class Changed_Names_Of_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAccount",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "ToAccount",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "DestAccount",
                table: "Transaction",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SrcAccount",
                table: "Transaction",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestAccount",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "SrcAccount",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "FromAccount",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ToAccount",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
