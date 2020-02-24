using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication6.Migrations
{
    public partial class cetvrta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clank_Administrator_AdministratorID",
                table: "Clank");

            migrationBuilder.DropForeignKey(
                name: "FK_NagradnaIgra_Administrator_AdministratorID",
                table: "NagradnaIgra");

            migrationBuilder.DropForeignKey(
                name: "FK_NagradnaIgra_Klijent_KlijentID",
                table: "NagradnaIgra");

            migrationBuilder.RenameColumn(
                name: "AdministratorID",
                table: "NagradnaIgra",
                newName: "KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_NagradnaIgra_AdministratorID",
                table: "NagradnaIgra",
                newName: "IX_NagradnaIgra_KorisnikId");

            migrationBuilder.RenameColumn(
                name: "AdministratorID",
                table: "Clank",
                newName: "KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_Clank_AdministratorID",
                table: "Clank",
                newName: "IX_Clank_KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clank_Korisnik_KorisnikId",
                table: "Clank",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NagradnaIgra_klijentT_KlijentID",
                table: "NagradnaIgra",
                column: "KlijentID",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NagradnaIgra_Korisnik_KorisnikId",
                table: "NagradnaIgra",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "KorisnikId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clank_Korisnik_KorisnikId",
                table: "Clank");

            migrationBuilder.DropForeignKey(
                name: "FK_NagradnaIgra_klijentT_KlijentID",
                table: "NagradnaIgra");

            migrationBuilder.DropForeignKey(
                name: "FK_NagradnaIgra_Korisnik_KorisnikId",
                table: "NagradnaIgra");

            migrationBuilder.RenameColumn(
                name: "KorisnikId",
                table: "NagradnaIgra",
                newName: "AdministratorID");

            migrationBuilder.RenameIndex(
                name: "IX_NagradnaIgra_KorisnikId",
                table: "NagradnaIgra",
                newName: "IX_NagradnaIgra_AdministratorID");

            migrationBuilder.RenameColumn(
                name: "KorisnikId",
                table: "Clank",
                newName: "AdministratorID");

            migrationBuilder.RenameIndex(
                name: "IX_Clank_KorisnikId",
                table: "Clank",
                newName: "IX_Clank_AdministratorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clank_Administrator_AdministratorID",
                table: "Clank",
                column: "AdministratorID",
                principalTable: "Administrator",
                principalColumn: "AdministratorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NagradnaIgra_Administrator_AdministratorID",
                table: "NagradnaIgra",
                column: "AdministratorID",
                principalTable: "Administrator",
                principalColumn: "AdministratorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NagradnaIgra_Klijent_KlijentID",
                table: "NagradnaIgra",
                column: "KlijentID",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
