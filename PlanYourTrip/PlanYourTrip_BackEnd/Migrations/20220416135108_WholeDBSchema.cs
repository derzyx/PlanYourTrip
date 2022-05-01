using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanYourTrip_BackEnd.Migrations
{
    public partial class WholeDBSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Nick = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IdAvatar = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Contributors",
                columns: table => new
                {
                    ContributorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserI_FK = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributors", x => x.ContributorId);
                    table.ForeignKey(
                        name: "FK_Contributors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Polubienia = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ZdjeciaJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.IdPost);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPlans",
                columns: table => new
                {
                    TripPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PunktyJSON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPlans", x => x.TripPlanId);
                    table.ForeignKey(
                        name: "FK_TripPlans_Users_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    IdAnswer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.IdAnswer);
                    table.ForeignKey(
                        name: "FK_Answers_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "IdPost",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Users_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ContributorsTripPlans",
                columns: table => new
                {
                    ContributorsContributorId = table.Column<int>(type: "int", nullable: false),
                    TripPlansTripPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorsTripPlans", x => new { x.ContributorsContributorId, x.TripPlansTripPlanId });
                    table.ForeignKey(
                        name: "FK_ContributorsTripPlans_Contributors_ContributorsContributorId",
                        column: x => x.ContributorsContributorId,
                        principalTable: "Contributors",
                        principalColumn: "ContributorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributorsTripPlans_TripPlans_TripPlansTripPlanId",
                        column: x => x.TripPlansTripPlanId,
                        principalTable: "TripPlans",
                        principalColumn: "TripPlanId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AutorId",
                table: "Answers",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PostId",
                table: "Answers",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributors_UserId",
                table: "Contributors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorsTripPlans_TripPlansTripPlanId",
                table: "ContributorsTripPlans",
                column: "TripPlansTripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AutorId",
                table: "Posts",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPlans_AutorId",
                table: "TripPlans",
                column: "AutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "ContributorsTripPlans");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropTable(
                name: "TripPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
