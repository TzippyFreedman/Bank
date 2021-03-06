﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionService.Data.Migrations
{
    public partial class Added_Date_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transaction",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transaction");
        }
    }
}
