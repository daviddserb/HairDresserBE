using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Appointments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        HairServices = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Appointments", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Customers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Customers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Employees",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Employees", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HairServices",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Duration = table.Column<TimeSpan>(type: "time", nullable: false),
            //        Price = table.Column<float>(type: "real", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HairServices", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkingDays",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DayId = table.Column<int>(type: "int", nullable: false),
            //        EmployeeId = table.Column<int>(type: "int", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //        EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkingDays", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Appointments");

            //migrationBuilder.DropTable(
            //    name: "Customers");

            //migrationBuilder.DropTable(
            //    name: "Employees");

            //migrationBuilder.DropTable(
            //    name: "HairServices");

            //migrationBuilder.DropTable(
            //    name: "WorkingDays");
        }
    }
}
