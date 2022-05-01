using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTrip_BackEnd.Migrations
{
    public partial class TripPlansTable_RemoveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOneDay",
                table: "TripPlans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOneDay",
                table: "TripPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
