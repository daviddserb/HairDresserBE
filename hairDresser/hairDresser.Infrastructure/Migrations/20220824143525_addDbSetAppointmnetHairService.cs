using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class addDbSetAppointmnetHairService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppointmentHairService_Appointments_AppointmentId",
            //    table: "AppointmentHairService");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppointmentHairService_HairServices_HairServiceId",
            //    table: "AppointmentHairService");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_AppointmentHairService",
            //    table: "AppointmentHairService");

            //migrationBuilder.RenameTable(
            //    name: "AppointmentHairService",
            //    newName: "AppointmentHairServices");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppointmentHairService_HairServiceId",
            //    table: "AppointmentHairServices",
            //    newName: "IX_AppointmentHairServices_HairServiceId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppointmentHairService_AppointmentId",
            //    table: "AppointmentHairServices",
            //    newName: "IX_AppointmentHairServices_AppointmentId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_AppointmentHairServices",
            //    table: "AppointmentHairServices",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppointmentHairServices_Appointments_AppointmentId",
            //    table: "AppointmentHairServices",
            //    column: "AppointmentId",
            //    principalTable: "Appointments",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppointmentHairServices_HairServices_HairServiceId",
            //    table: "AppointmentHairServices",
            //    column: "HairServiceId",
            //    principalTable: "HairServices",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppointmentHairServices_Appointments_AppointmentId",
            //    table: "AppointmentHairServices");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_AppointmentHairServices_HairServices_HairServiceId",
            //    table: "AppointmentHairServices");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_AppointmentHairServices",
            //    table: "AppointmentHairServices");

            //migrationBuilder.RenameTable(
            //    name: "AppointmentHairServices",
            //    newName: "AppointmentHairService");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppointmentHairServices_HairServiceId",
            //    table: "AppointmentHairService",
            //    newName: "IX_AppointmentHairService_HairServiceId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_AppointmentHairServices_AppointmentId",
            //    table: "AppointmentHairService",
            //    newName: "IX_AppointmentHairService_AppointmentId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_AppointmentHairService",
            //    table: "AppointmentHairService",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppointmentHairService_Appointments_AppointmentId",
            //    table: "AppointmentHairService",
            //    column: "AppointmentId",
            //    principalTable: "Appointments",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AppointmentHairService_HairServices_HairServiceId",
            //    table: "AppointmentHairService",
            //    column: "HairServiceId",
            //    principalTable: "HairServices",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
