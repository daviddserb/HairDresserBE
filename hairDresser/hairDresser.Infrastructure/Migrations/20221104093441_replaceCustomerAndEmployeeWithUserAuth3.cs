using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class replaceCustomerAndEmployeeWithUserAuth3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_AspNetUsers_UserId",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_UserId",
            //    table: "Appointments");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "UserId",
            //    table: "Appointments",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId1",
            //    table: "Appointments",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_UserId1",
            //    table: "Appointments",
            //    column: "UserId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Appointments_AspNetUsers_UserId1",
            //    table: "Appointments",
            //    column: "UserId1",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Appointments_AspNetUsers_UserId1",
            //    table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_UserId1",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "Appointments");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "Appointments",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

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
        }
    }
}
