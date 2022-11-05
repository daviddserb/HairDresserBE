using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class replaceCustomerAndEmployeeWithUserAuth2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkingIntervals_AspNetUsers_UserId",
            //    table: "WorkingIntervals");

            //migrationBuilder.DropIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "AspNetUserRoles");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "WorkingIntervals",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkingIntervals_AspNetUsers_UserId",
            //    table: "WorkingIntervals",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_WorkingIntervals_AspNetUsers_UserId",
            //    table: "WorkingIntervals");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "WorkingIntervals",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId1",
            //    table: "AspNetUserRoles",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
            //    table: "AspNetUserRoles",
            //    column: "UserId1",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WorkingIntervals_AspNetUsers_UserId",
            //    table: "WorkingIntervals",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");
        }
    }
}
