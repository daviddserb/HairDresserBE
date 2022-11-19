using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class addDayTableAndChangeWorkingDayTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Name",
            //    table: "WorkingDays");

            //migrationBuilder.AlterColumn<string>(
            //    name: "EmployeeName",
            //    table: "Appointments",
            //    type: "nvarchar(20)",
            //    maxLength: 20,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Days");

            //migrationBuilder.AddColumn<string>(
            //    name: "Name",
            //    table: "WorkingDays",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "EmployeeName",
            //    table: "Appointments",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(20)",
            //    oldMaxLength: 20);
        }
    }
}
