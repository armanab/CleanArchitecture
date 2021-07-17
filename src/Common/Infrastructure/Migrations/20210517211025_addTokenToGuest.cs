using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanApplication.Infrastructure.Migrations
{
    public partial class addTokenToGuest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Guests",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Guests");
        }
    }
}
