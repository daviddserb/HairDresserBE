using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class replaceCustomerAndEmployeeWithUserAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeesHairServices_Employees_EmployeeId",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkingIntervals_Employees_EmployeeId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropIndex(
            //    name: "IX_WorkingIntervals_EmployeeId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeesHairServices_EmployeeId",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_CustomerId",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_EmployeeId",
            //    table: "Appointments");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "EmployeeId",
            //    table: "WorkingIntervals",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddColumn<int>(
            //    name: "EmployeeId1",
            //    table: "WorkingIntervals",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "WorkingIntervals",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "EmployeeId",
            //    table: "EmployeesHairServices",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddColumn<int>(
            //    name: "EmployeeId1",
            //    table: "EmployeesHairServices",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "EmployeesHairServices",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId1",
            //    table: "AspNetUserRoles",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "EmployeeId",
            //    table: "Appointments",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "CustomerId",
            //    table: "Appointments",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddColumn<int>(
            //    name: "CustomerId1",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "EmployeeId1",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "Appointments",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkingIntervals_EmployeeId1",
            //    table: "WorkingIntervals",
            //    column: "EmployeeId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkingIntervals_UserId",
            //    table: "WorkingIntervals",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeesHairServices_EmployeeId1",
            //    table: "EmployeesHairServices",
            //    column: "EmployeeId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeesHairServices_UserId",
            //    table: "EmployeesHairServices",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_CustomerId1",
            //    table: "Appointments",
            //    column: "CustomerId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_EmployeeId1",
            //    table: "Appointments",
            //    column: "EmployeeId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_UserId",
            //    table: "Appointments",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_AspNetUsers_UserId",
            //    table: "Appointments",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId1",
            //    table: "Appointments",
            //    column: "CustomerId1",
            //    principalTable: "Customers",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId1",
            //    table: "Appointments",
            //    column: "EmployeeId1",
            //    principalTable: "Employees",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeesHairServices_AspNetUsers_UserId",
            //    table: "EmployeesHairServices",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeesHairServices_Employees_EmployeeId1",
            //    table: "EmployeesHairServices",
            //    column: "EmployeeId1",
            //    principalTable: "Employees",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkingIntervals_AspNetUsers_UserId",
            //    table: "WorkingIntervals",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkingIntervals_Employees_EmployeeId1",
            //    table: "WorkingIntervals",
            //    column: "EmployeeId1",
            //    principalTable: "Employees",
            //    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_AspNetUsers_UserId",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId1",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId1",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeesHairServices_AspNetUsers_UserId",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeesHairServices_Employees_EmployeeId1",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkingIntervals_AspNetUsers_UserId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkingIntervals_Employees_EmployeeId1",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropIndex(
            //    name: "IX_WorkingIntervals_EmployeeId1",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropIndex(
            //    name: "IX_WorkingIntervals_UserId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeesHairServices_EmployeeId1",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeesHairServices_UserId",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_CustomerId1",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_EmployeeId1",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_UserId",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "EmployeeId1",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropColumn(
            //    name: "EmployeeId1",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "EmployeesHairServices");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropColumn(
            //    name: "CustomerId1",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "EmployeeId1",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "Appointments");

            //migrationBuilder.AlterColumn<int>(
            //    name: "EmployeeId",
            //    table: "WorkingIntervals",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

            //migrationBuilder.AlterColumn<int>(
            //    name: "EmployeeId",
            //    table: "EmployeesHairServices",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

            //migrationBuilder.AlterColumn<int>(
            //    name: "EmployeeId",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

            //migrationBuilder.AlterColumn<int>(
            //    name: "CustomerId",
            //    table: "Appointments",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkingIntervals_EmployeeId",
            //    table: "WorkingIntervals",
            //    column: "EmployeeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EmployeesHairServices_EmployeeId",
            //    table: "EmployeesHairServices",
            //    column: "EmployeeId");

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

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeesHairServices_Employees_EmployeeId",
            //    table: "EmployeesHairServices",
            //    column: "EmployeeId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkingIntervals_Employees_EmployeeId",
            //    table: "WorkingIntervals",
            //    column: "EmployeeId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
