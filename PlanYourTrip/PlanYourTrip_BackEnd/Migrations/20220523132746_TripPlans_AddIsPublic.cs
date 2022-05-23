using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTrip_BackEnd.Migrations
{
    public partial class TripPlans_AddIsPublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Publiczny",
                table: "TripPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publiczny",
                table: "TripPlans");
        }
    }
}
