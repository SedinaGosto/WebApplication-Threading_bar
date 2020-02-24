using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication6.Migrations
{
    public partial class dvanaest13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorizacijskiTokenKlijent_klijentT_KlijentId",
                table: "AutorizacijskiTokenKlijent");

            migrationBuilder.DropForeignKey(
                name: "FK_KlijentNotifikacija_klijentT_KlijentId",
                table: "KlijentNotifikacija");

            migrationBuilder.DropForeignKey(
                name: "FK_klijentT_Grad_GradID",
                table: "klijentT");

            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_klijentT_KlijentId",
                table: "Komentar");

            migrationBuilder.DropForeignKey(
                name: "FK_NagradnaIgra_klijentT_KlijentID",
                table: "NagradnaIgra");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_klijentT_KlijentId",
                table: "Ocjena");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_klijentT_KlijentId",
                table: "Rezervacija");

            migrationBuilder.DropPrimaryKey(
                name: "PK_klijentT",
                table: "klijentT");

            migrationBuilder.RenameTable(
                name: "klijentT",
                newName: "Klijent");

            migrationBuilder.RenameIndex(
                name: "IX_klijentT_GradID",
                table: "Klijent",
                newName: "IX_Klijent_GradID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Klijent",
                table: "Klijent",
                column: "KlijentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorizacijskiTokenKlijent_Klijent_KlijentId",
                table: "AutorizacijskiTokenKlijent",
                column: "KlijentId",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Klijent_Grad_GradID",
                table: "Klijent",
                column: "GradID",
                principalTable: "Grad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KlijentNotifikacija_Klijent_KlijentId",
                table: "KlijentNotifikacija",
                column: "KlijentId",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_Klijent_KlijentId",
                table: "Komentar",
                column: "KlijentId",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NagradnaIgra_Klijent_KlijentID",
                table: "NagradnaIgra",
                column: "KlijentID",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Klijent_KlijentId",
                table: "Ocjena",
                column: "KlijentId",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Klijent_KlijentId",
                table: "Rezervacija",
                column: "KlijentId",
                principalTable: "Klijent",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutorizacijskiTokenKlijent_Klijent_KlijentId",
                table: "AutorizacijskiTokenKlijent");

            migrationBuilder.DropForeignKey(
                name: "FK_Klijent_Grad_GradID",
                table: "Klijent");

            migrationBuilder.DropForeignKey(
                name: "FK_KlijentNotifikacija_Klijent_KlijentId",
                table: "KlijentNotifikacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_Klijent_KlijentId",
                table: "Komentar");

            migrationBuilder.DropForeignKey(
                name: "FK_NagradnaIgra_Klijent_KlijentID",
                table: "NagradnaIgra");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Klijent_KlijentId",
                table: "Ocjena");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Klijent_KlijentId",
                table: "Rezervacija");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Klijent",
                table: "Klijent");

            migrationBuilder.RenameTable(
                name: "Klijent",
                newName: "klijentT");

            migrationBuilder.RenameIndex(
                name: "IX_Klijent_GradID",
                table: "klijentT",
                newName: "IX_klijentT_GradID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_klijentT",
                table: "klijentT",
                column: "KlijentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AutorizacijskiTokenKlijent_klijentT_KlijentId",
                table: "AutorizacijskiTokenKlijent",
                column: "KlijentId",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KlijentNotifikacija_klijentT_KlijentId",
                table: "KlijentNotifikacija",
                column: "KlijentId",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_klijentT_Grad_GradID",
                table: "klijentT",
                column: "GradID",
                principalTable: "Grad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_klijentT_KlijentId",
                table: "Komentar",
                column: "KlijentId",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NagradnaIgra_klijentT_KlijentID",
                table: "NagradnaIgra",
                column: "KlijentID",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_klijentT_KlijentId",
                table: "Ocjena",
                column: "KlijentId",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_klijentT_KlijentId",
                table: "Rezervacija",
                column: "KlijentId",
                principalTable: "klijentT",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
