using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class addManyToManyEmployeeHairService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeHairService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    HairServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHairService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeHairService_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHairService_HairServices_HairServiceId",
                        column: x => x.HairServiceId,
                        principalTable: "HairServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHairService_EmployeeId",
                table: "EmployeeHairService",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHairService_HairServiceId",
                table: "EmployeeHairService",
                column: "HairServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeHairService");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
