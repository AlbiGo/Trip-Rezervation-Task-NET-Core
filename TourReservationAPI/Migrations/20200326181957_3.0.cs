using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourReservationAPI.Migrations
{
    public partial class _30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rezervations",
                columns: table => new
                {
                    RezervationID = table.Column<Guid>(nullable: false),
                    RezervationDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    PersonAge = table.Column<int>(nullable: false),
                    GuideId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervations", x => x.RezervationID);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    guideID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Pice = table.Column<decimal>(nullable: false),
                    AgeLimit = table.Column<int>(nullable: false),
                    RezervationID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.guideID);
                    table.ForeignKey(
                        name: "FK_Guides_Rezervations_RezervationID",
                        column: x => x.RezervationID,
                        principalTable: "Rezervations",
                        principalColumn: "RezervationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guides_RezervationID",
                table: "Guides",
                column: "RezervationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Rezervations");
        }
    }
}
