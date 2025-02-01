using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class changeNameFromWorkingDaysToWorkingIntervals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "WorkingDays");

            //migrationBuilder.CreateTable(
            //    name: "WorkingIntervals",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DayId = table.Column<int>(type: "int", nullable: false),
            //        EmployeeId = table.Column<int>(type: "int", nullable: false),
            //        StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //        EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkingIntervals", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WorkingIntervals_Days_DayId",
            //            column: x => x.DayId,
            //            principalTable: "Days",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_WorkingIntervals_Employees_EmployeeId",
            //            column: x => x.EmployeeId,
            //            principalTable: "Employees",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkingIntervals_DayId",
            //    table: "WorkingIntervals",
            //    column: "DayId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkingIntervals_EmployeeId",
            //    table: "WorkingIntervals",
            //    column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "WorkingIntervals");

            //migrationBuilder.CreateTable(
            //    name: "WorkingDays",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DayId = table.Column<int>(type: "int", nullable: false),
            //        EmployeeId = table.Column<int>(type: "int", nullable: false),
            //        EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //        StartTime = table.Column<TimeSpan>(type: "time", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkingDays", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WorkingDays_Days_DayId",
            //            column: x => x.DayId,
            //            principalTable: "Days",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_WorkingDays_Employees_EmployeeId",
            //            column: x => x.EmployeeId,
            //            principalTable: "Employees",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkingDays_DayId",
            //    table: "WorkingDays",
            //    column: "DayId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkingDays_EmployeeId",
            //    table: "WorkingDays",
            //    column: "EmployeeId");
        }
    }
}
