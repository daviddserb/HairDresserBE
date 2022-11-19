using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class AddPriceAndIsDeletedInAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<float>(
            //    name: "Price",
            //    table: "Appointments",
            //    type: "real",
            //    nullable: false,
            //    defaultValue: 0f);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "isDeleted",
            //    table: "Appointments",
            //    type: "datetime2",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Price",
            //    table: "Appointments");

            //migrationBuilder.DropColumn(
            //    name: "isDeleted",
            //    table: "Appointments");
        }
    }
}
