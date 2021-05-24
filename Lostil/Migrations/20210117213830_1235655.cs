using Microsoft.EntityFrameworkCore.Migrations;

namespace Lostil.Migrations
{
    public partial class _1235655 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Orders",
                newName: "UserId");
        }
    }
}
