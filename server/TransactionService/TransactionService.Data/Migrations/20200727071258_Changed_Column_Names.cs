using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionService.Data.Migrations
{
    public partial class Changed_Column_Names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestAccount",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "SrcAccount",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "DestAccountId",
                table: "Transaction",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SrcAccountId",
                table: "Transaction",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestAccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "SrcAccountId",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "DestAccount",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SrcAccount",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
