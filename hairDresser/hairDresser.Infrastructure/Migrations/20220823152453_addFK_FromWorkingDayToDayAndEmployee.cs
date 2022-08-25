using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class addFK_FromWorkingDayToDayAndEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_DayId",
                table: "WorkingDays",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDays_EmployeeId",
                table: "WorkingDays",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_Days_DayId",
                table: "WorkingDays",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_Employees_EmployeeId",
                table: "WorkingDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_Days_DayId",
                table: "WorkingDays");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_Employees_EmployeeId",
                table: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDays_DayId",
                table: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingDays_EmployeeId",
                table: "WorkingDays");
        }
    }
}
