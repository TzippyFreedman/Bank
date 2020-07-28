using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Data.Migrations
{
    public partial class AddedFailedOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryOperation");

            migrationBuilder.CreateTable(
                name: "SucceededHistoryOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    AccountId = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    IsCredit = table.Column<bool>(nullable: false),
                    TransactionAmount = table.Column<int>(nullable: false),
                    Balance = table.Column<int>(nullable: false),
                    OperationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucceededHistoryOperation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SucceededHistoryOperation");

            migrationBuilder.CreateTable(
                name: "HistoryOperation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    OperationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionAmount = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryOperation", x => x.Id);
                });
        }
    }
}
