using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirportWeatherForecastWorkerService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TAFs",
                columns: table => new
                {
                    AirportCode = table.Column<int>(type: "INTEGER", nullable: false),
                    IssueTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RawTAF = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAFs", x => new { x.AirportCode, x.IssueTime });
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAFs_AirportCode_IssueTime",
                table: "TAFs",
                columns: new[] { "AirportCode", "IssueTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAFs");
        }
    }
}
