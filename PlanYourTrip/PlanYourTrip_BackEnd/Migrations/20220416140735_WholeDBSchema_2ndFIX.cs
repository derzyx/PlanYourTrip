using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTrip_BackEnd.Migrations
{
    public partial class WholeDBSchema_2ndFIX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContributorsTripPlans_TripPlans_TripPlansTripPlanId",
                table: "ContributorsTripPlans");

            migrationBuilder.DropIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContributorsTripPlans_TripPlans_TripPlansTripPlanId",
                table: "ContributorsTripPlans",
                column: "TripPlansTripPlanId",
                principalTable: "TripPlans",
                principalColumn: "TripPlanId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContributorsTripPlans_TripPlans_TripPlansTripPlanId",
                table: "ContributorsTripPlans");

            migrationBuilder.DropIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContributorsTripPlans_TripPlans_TripPlansTripPlanId",
                table: "ContributorsTripPlans",
                column: "TripPlansTripPlanId",
                principalTable: "TripPlans",
                principalColumn: "TripPlanId");
        }
    }
}
