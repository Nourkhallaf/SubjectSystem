using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICore.Migrations
{
    public partial class gradeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "StudentsSubjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentsSubjects");
        }
    }
}
