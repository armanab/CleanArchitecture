using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanApplication.Infrastructure.Migrations
{
    public partial class updateFixextensionColumnNameImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "extention",
                table: "Images",
                newName: "extension");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "extension",
                table: "Images",
                newName: "extention");
        }
    }
}
