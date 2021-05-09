using Microsoft.EntityFrameworkCore.Migrations;

namespace VisitorManagementSystemMVC.Migrations
{
    public partial class VisitorDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Rejected",
                table: "Visitors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "Visitors");
        }
    }
}
