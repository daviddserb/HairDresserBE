using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class changeNameInWorkingIntervalOfColumnDayToWorkingDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingIntervals_WorkingDays_DayId",
                table: "WorkingIntervals");

            migrationBuilder.RenameColumn(
                name: "DayId",
                table: "WorkingIntervals",
                newName: "WorkingDayId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingIntervals_DayId",
                table: "WorkingIntervals",
                newName: "IX_WorkingIntervals_WorkingDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingIntervals_WorkingDays_WorkingDayId",
                table: "WorkingIntervals",
                column: "WorkingDayId",
                principalTable: "WorkingDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingIntervals_WorkingDays_WorkingDayId",
                table: "WorkingIntervals");

            migrationBuilder.RenameColumn(
                name: "WorkingDayId",
                table: "WorkingIntervals",
                newName: "DayId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkingIntervals_WorkingDayId",
                table: "WorkingIntervals",
                newName: "IX_WorkingIntervals_DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingIntervals_WorkingDays_DayId",
                table: "WorkingIntervals",
                column: "DayId",
                principalTable: "WorkingDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
