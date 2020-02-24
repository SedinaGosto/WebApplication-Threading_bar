using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication6.Migrations
{
    public partial class osma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KlijentNotifikacija",
                columns: table => new
                {
                    NotifikacijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RezervacijaId = table.Column<int>(nullable: false),
                    KlijentId = table.Column<int>(nullable: false),
                    DatumSlanja = table.Column<DateTime>(nullable: false),
                    IsProcitano = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KlijentNotifikacija", x => x.NotifikacijaId);
                    table.ForeignKey(
                        name: "FK_KlijentNotifikacija_klijentT_KlijentId",
                        column: x => x.KlijentId,
                        principalTable: "klijentT",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KlijentNotifikacija_Rezervacija_RezervacijaId",
                        column: x => x.RezervacijaId,
                        principalTable: "Rezervacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KlijentNotifikacija_KlijentId",
                table: "KlijentNotifikacija",
                column: "KlijentId");

            migrationBuilder.CreateIndex(
                name: "IX_KlijentNotifikacija_RezervacijaId",
                table: "KlijentNotifikacija",
                column: "RezervacijaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KlijentNotifikacija");
        }
    }
}
