using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanApplication.Infrastructure.Migrations
{
    public partial class changeContentAndSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Contents",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Contents",
                newName: "Priority");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Contents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slot",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Slot",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Contents",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Contents",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Contents",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Title",
                table: "Contents",
                type: "int",
                maxLength: 200,
                nullable: false,
                defaultValue: 0);
        }
    }
}
