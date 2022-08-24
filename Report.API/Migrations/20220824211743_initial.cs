using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    uuId = table.Column<Guid>(type: "uuid", nullable: false),
                    requestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reportStatus = table.Column<int>(type: "integer", nullable: false),
                    reportPath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.uuId);
                });

            migrationBuilder.CreateTable(
                name: "ReportDetails",
                columns: table => new
                {
                    uuId = table.Column<Guid>(type: "uuid", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    personCount = table.Column<int>(type: "integer", nullable: false),
                    phoneNumberCount = table.Column<int>(type: "integer", nullable: false),
                    reportUuId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDetails", x => x.uuId);
                    table.ForeignKey(
                        name: "FK_ReportDetails_Reports_reportUuId",
                        column: x => x.reportUuId,
                        principalTable: "Reports",
                        principalColumn: "uuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetails_reportUuId",
                table: "ReportDetails",
                column: "reportUuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportDetails");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
