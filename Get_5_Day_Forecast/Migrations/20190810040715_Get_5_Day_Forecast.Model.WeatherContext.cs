using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Get_5_Day_Forecast.Migrations
{
    public partial class Get_5_Day_ForecastModelWeatherContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvgDayForecasts",
                columns: table => new
                {
                    ForecastId = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    AvgMaxTemp = table.Column<decimal>(nullable: false),
                    AvgMinTemp = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvgDayForecasts", x => x.ForecastId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvgDayForecasts");
        }
    }
}
