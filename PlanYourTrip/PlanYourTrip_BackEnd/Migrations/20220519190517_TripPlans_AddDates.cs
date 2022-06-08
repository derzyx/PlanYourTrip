using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTrip_BackEnd.Migrations
{
    public partial class TripPlans_AddDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataUtworzenia",
                table: "TripPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OstatniaAktualizacja",
                table: "TripPlans",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataUtworzenia",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "OstatniaAktualizacja",
                table: "TripPlans");
        }
    }
}
