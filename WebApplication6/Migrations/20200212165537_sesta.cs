using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication6.Migrations
{
    public partial class sesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Klijent_KlijentId",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Termin_Uposlenik_UposlenikId",
                table: "Termin");

            migrationBuilder.DropColumn(
                name: "AdresaSlike",
                table: "Clank");

            migrationBuilder.RenameColumn(
                name: "UposlenikId",
                table: "Termin",
                newName: "KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_Termin_UposlenikId",
                table: "Termin",
                newName: "IX_Termin_KorisnikId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Slika",
                table: "Clank",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_klijentT_KlijentId",
                table: "Rezervacija",
                column: "KlijentId",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_Korisnik_KorisnikId",
                table: "Termin",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_klijentT_KlijentId",
                table: "Rezervacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Termin_Korisnik_KorisnikId",
                table: "Termin");

            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Clank");

            migrationBuilder.RenameColumn(
                name: "KorisnikId",
                table: "Termin",
                newName: "UposlenikId");

            migrationBuilder.RenameIndex(
                name: "IX_Termin_KorisnikId",
                table: "Termin",
                newName: "IX_Termin_UposlenikId");

            migrationBuilder.AddColumn<string>(
                name: "AdresaSlike",
                table: "Clank",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Klijent_KlijentId",
                table: "Rezervacija",
                column: "KlijentId",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Termin_Uposlenik_UposlenikId",
                table: "Termin",
                column: "UposlenikId",
                principalTable: "Uposlenik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
