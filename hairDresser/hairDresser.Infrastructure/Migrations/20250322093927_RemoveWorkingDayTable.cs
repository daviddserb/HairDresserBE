using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hairDresser.Infrastructure.Migrations
{
    public partial class RemoveWorkingDayTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingIntervals_WorkingDays_WorkingDayId",
                table: "WorkingIntervals");

            migrationBuilder.DropTable(
                name: "WorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkingIntervals_WorkingDayId",
                table: "WorkingIntervals");

            migrationBuilder.RenameColumn(
                name: "WorkingDayId",
                table: "WorkingIntervals",
                newName: "WorkingDay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkingDay",
                table: "WorkingIntervals",
                newName: "WorkingDayId");

            migrationBuilder.CreateTable(
                name: "WorkingDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingIntervals_WorkingDayId",
                table: "WorkingIntervals",
                column: "WorkingDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingIntervals_WorkingDays_WorkingDayId",
                table: "WorkingIntervals",
                column: "WorkingDayId",
                principalTable: "WorkingDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
