using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class addDbSetEmployeeHairService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeHairService_Employees_EmployeeId",
            //    table: "EmployeeHairService");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeHairService_HairServices_HairServiceId",
            //    table: "EmployeeHairService");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_EmployeeHairService",
            //    table: "EmployeeHairService");

            //migrationBuilder.RenameTable(
            //    name: "EmployeeHairService",
            //    newName: "EmployeeHairServices");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EmployeeHairService_HairServiceId",
            //    table: "EmployeeHairServices",
            //    newName: "IX_EmployeeHairServices_HairServiceId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EmployeeHairService_EmployeeId",
            //    table: "EmployeeHairServices",
            //    newName: "IX_EmployeeHairServices_EmployeeId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_EmployeeHairServices",
            //    table: "EmployeeHairServices",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeHairServices_Employees_EmployeeId",
            //    table: "EmployeeHairServices",
            //    column: "EmployeeId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeHairServices_HairServices_HairServiceId",
            //    table: "EmployeeHairServices",
            //    column: "HairServiceId",
            //    principalTable: "HairServices",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeHairServices_Employees_EmployeeId",
            //    table: "EmployeeHairServices");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeHairServices_HairServices_HairServiceId",
            //    table: "EmployeeHairServices");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_EmployeeHairServices",
            //    table: "EmployeeHairServices");

            //migrationBuilder.RenameTable(
            //    name: "EmployeeHairServices",
            //    newName: "EmployeeHairService");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EmployeeHairServices_HairServiceId",
            //    table: "EmployeeHairService",
            //    newName: "IX_EmployeeHairService_HairServiceId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_EmployeeHairServices_EmployeeId",
            //    table: "EmployeeHairService",
            //    newName: "IX_EmployeeHairService_EmployeeId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_EmployeeHairService",
            //    table: "EmployeeHairService",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeHairService_Employees_EmployeeId",
            //    table: "EmployeeHairService",
            //    column: "EmployeeId",
            //    principalTable: "Employees",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeHairService_HairServices_HairServiceId",
            //    table: "EmployeeHairService",
            //    column: "HairServiceId",
            //    principalTable: "HairServices",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
