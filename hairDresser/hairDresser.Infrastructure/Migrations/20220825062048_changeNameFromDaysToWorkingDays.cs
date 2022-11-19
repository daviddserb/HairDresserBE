using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class changeNameFromDaysToWorkingDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkingIntervals_Days_DayId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropTable(
            //    name: "Days");

            //migrationBuilder.CreateTable(
            //    name: "WorkingDays",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkingDays", x => x.Id);
            //    });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkingIntervals_WorkingDays_DayId",
            //    table: "WorkingIntervals",
            //    column: "DayId",
            //    principalTable: "WorkingDays",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkingIntervals_WorkingDays_DayId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropTable(
            //    name: "WorkingDays");

            //migrationBuilder.CreateTable(
            //    name: "Days",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Days", x => x.Id);
            //    });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkingIntervals_Days_DayId",
            //    table: "WorkingIntervals",
            //    column: "DayId",
            //    principalTable: "Days",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
