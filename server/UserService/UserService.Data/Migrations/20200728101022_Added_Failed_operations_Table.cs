using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Data.Migrations
{
    public partial class Added_Failed_operations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoryOperation_AccountId",
                table: "HistoryOperation");

            migrationBuilder.CreateTable(
                name: "FailedHistoryOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    AccountId = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    OperationTime = table.Column<DateTime>(nullable: false),
                    IsCredit = table.Column<bool>(nullable: false),
                    TransactionAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailedHistoryOperation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FailedHistoryOperation");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryOperation_AccountId",
                table: "HistoryOperation",
                column: "AccountId",
                unique: true);
        }
    }
}
