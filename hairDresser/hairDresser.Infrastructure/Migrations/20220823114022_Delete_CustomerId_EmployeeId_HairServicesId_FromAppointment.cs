using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class Delete_CustomerId_EmployeeId_HairServicesId_FromAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId1",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId1",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_CustomerId1",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_EmployeeId1",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "CustomerId1",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "EmployeeId1",
            //    table: "Appointments");

            //migrationBuilder.AlterColumn<int>(
            //    name: "EmployeeId",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)");

            //migrationBuilder.AlterColumn<int>(
            //    name: "CustomerId",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_CustomerId",
            //    table: "Appointments",
            //    column: "CustomerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_EmployeeId",
            //    table: "Appointments",
            //    column: "EmployeeId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId",
            //    table: "Appointments",
            //    column: "CustomerId",
            //    principalTable: "Customers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId",
            //    table: "Appointments",
            //    column: "EmployeeId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_CustomerId",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_EmployeeId",
            //    table: "Appointments");

            //migrationBuilder.AlterColumn<string>(
            //    name: "EmployeeId",
            //    table: "Appointments",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AlterColumn<string>(
            //    name: "CustomerId",
            //    table: "Appointments",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddColumn<int>(
            //    name: "CustomerId1",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "EmployeeId1",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_CustomerId1",
            //    table: "Appointments",
            //    column: "CustomerId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_EmployeeId1",
            //    table: "Appointments",
            //    column: "EmployeeId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId1",
            //    table: "Appointments",
            //    column: "CustomerId1",
            //    principalTable: "Customers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId1",
            //    table: "Appointments",
            //    column: "EmployeeId1",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
