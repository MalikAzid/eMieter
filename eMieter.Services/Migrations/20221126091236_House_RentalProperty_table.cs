using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eMieter.Services.Migrations
{
    public partial class House_RentalProperty_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblHouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblHouse_tblOwner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "tblOwner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblRentalProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EWID = table.Column<string>(nullable: true),
                    LivingAreaSquareMeters = table.Column<decimal>(nullable: false),
                    RoomCount = table.Column<decimal>(nullable: false),
                    Floor = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRentalProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblRentalProperty_tblHouse_HouseId",
                        column: x => x.HouseId,
                        principalTable: "tblHouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblHouse_OwnerId",
                table: "tblHouse",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRentalProperty_HouseId",
                table: "tblRentalProperty",
                column: "HouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblRentalProperty");

            migrationBuilder.DropTable(
                name: "tblHouse");
        }
    }
}
