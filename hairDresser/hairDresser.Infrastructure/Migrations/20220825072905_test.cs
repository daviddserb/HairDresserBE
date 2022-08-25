using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "WorkingDays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_EmployeeId",
                table: "WorkingDays",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_Employees_EmployeeId",
                table: "WorkingDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_Employees_EmployeeId",
                table: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDays_EmployeeId",
                table: "WorkingDays");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "WorkingDays");
        }
    }
}
