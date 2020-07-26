using Microsoft.EntityFrameworkCore.Migrations;

namespace TransferService.Data.Migrations
{
    public partial class Added_FailureReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FailureReason",
                table: "Transfer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailureReason",
                table: "Transfer");
        }
    }
}
