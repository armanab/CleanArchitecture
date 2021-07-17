using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanApplication.Infrastructure.Migrations
{
    public partial class removeInstructorOnStoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
              name: "IX_Stories_InstructorId",
              table: "Stories");
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Instructors_InstructorId",
                table: "Stories");

          

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Stories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Stories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stories_InstructorId",
                table: "Stories",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Instructors_InstructorId",
                table: "Stories",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
