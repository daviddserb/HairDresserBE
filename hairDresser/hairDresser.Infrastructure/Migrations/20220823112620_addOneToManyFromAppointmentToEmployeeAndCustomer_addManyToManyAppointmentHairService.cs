using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class addOneToManyFromAppointmentToEmployeeAndCustomer_addManyToManyAppointmentHairService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.DropColumn(
            //    name: "CustomerName",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "EmployeeName",
            //    table: "Appointments");

            //migrationBuilder.RenameColumn(
            //    name: "HairServices",
            //    table: "Appointments",
            //    newName: "HairServicesId");

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

            //migrationBuilder.CreateTable(
            //    name: "AppointmentHairService",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AppointmentId = table.Column<int>(type: "int", nullable: false),
            //        HairServiceId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AppointmentHairService", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AppointmentHairService_Appointments_AppointmentId",
            //            column: x => x.AppointmentId,
            //            principalTable: "Appointments",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AppointmentHairService_HairServices_HairServiceId",
            //            column: x => x.HairServiceId,
            //            principalTable: "HairServices",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_CustomerId1",
            //    table: "Appointments",
            //    column: "CustomerId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_EmployeeId1",
            //    table: "Appointments",
            //    column: "EmployeeId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AppointmentHairService_AppointmentId",
            //    table: "AppointmentHairService",
            //    column: "AppointmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AppointmentHairService_HairServiceId",
            //    table: "AppointmentHairService",
            //    column: "HairServiceId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Customers_CustomerId1",
            //    table: "Appointments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_Employees_EmployeeId1",
            //    table: "Appointments");

            //migrationBuilder.DropTable(
            //    name: "AppointmentHairService");

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

            //migrationBuilder.RenameColumn(
            //    name: "HairServicesId",
            //    table: "Appointments",
            //    newName: "HairServices");

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

            //migrationBuilder.AddColumn<string>(
            //    name: "CustomerName",
            //    table: "Appointments",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "EmployeeName",
            //    table: "Appointments",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

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
    }
}
