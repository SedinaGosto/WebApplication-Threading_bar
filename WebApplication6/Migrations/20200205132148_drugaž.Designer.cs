﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication6.EF;

namespace WebApplication6.Migrations
{
    [DbContext(typeof(MojContext))]
    [Migration("20200205132148_drugaž")]
    partial class drugaž
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication6.Models.Admin", b =>
                {
                    b.Property<int>("AdministratorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .IsRequired();

                    b.Property<string>("BrojTelefona")
                        .IsRequired();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<int?>("KorisnickiNalogId");

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.HasKey("AdministratorID");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("WebApplication6.Models.AutorizacijskiToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IpAdresa");

                    b.Property<int>("KorisnickiNalogId");

                    b.Property<string>("Vrijednost");

                    b.Property<DateTime>("VrijemeEvidentiranja");

                    b.HasKey("Id");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("AutorizacijskiToken");
                });

            modelBuilder.Entity("WebApplication6.Models.Clanak", b =>
                {
                    b.Property<int>("ClanakID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdministratorID");

                    b.Property<string>("AdresaSlike");

                    b.Property<int>("ClanciKategorijaID");

                    b.Property<DateTime>("DatumObjave");

                    b.Property<string>("Naslov");

                    b.Property<string>("Podnaslov");

                    b.Property<string>("TekstClanka");

                    b.HasKey("ClanakID");

                    b.HasIndex("AdministratorID");

                    b.HasIndex("ClanciKategorijaID");

                    b.ToTable("Clank");
                });

            modelBuilder.Entity("WebApplication6.Models.ClanciKategorija", b =>
                {
                    b.Property<int>("ClanciKategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumKreiranja");

                    b.Property<string>("Naziv");

                    b.HasKey("ClanciKategorijaID");

                    b.ToTable("ClanciKategorija");
                });

            modelBuilder.Entity("WebApplication6.Models.Grad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.HasKey("Id");

                    b.ToTable("Grad");
                });

            modelBuilder.Entity("WebApplication6.Models.Klijent", b =>
                {
                    b.Property<int>("KlijentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrojTelefona");

                    b.Property<string>("Email");

                    b.Property<int>("GradID");

                    b.Property<string>("Ime");

                    b.Property<int?>("KorisnickiNalogId");

                    b.Property<string>("Prezime");

                    b.HasKey("KlijentID");

                    b.HasIndex("GradID");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("Klijent");
                });

            modelBuilder.Entity("WebApplication6.Models.KorisnickiNalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("KorisnickoIme");

                    b.Property<string>("Lozinka");

                    b.HasKey("Id");

                    b.ToTable("KorisnickiNalog");
                });

            modelBuilder.Entity("WebApplication6.Models.Nagrada", b =>
                {
                    b.Property<int>("NagradaID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv");

                    b.HasKey("NagradaID");

                    b.ToTable("Nagrada");
                });

            modelBuilder.Entity("WebApplication6.Models.NagradnaIgra", b =>
                {
                    b.Property<int>("NagradnaIgraID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdministratorID");

                    b.Property<DateTime>("DatumPocetka");

                    b.Property<DateTime>("DatumZavrsetka");

                    b.Property<int>("KlijentID");

                    b.Property<int>("NagradaID");

                    b.Property<string>("Opis");

                    b.HasKey("NagradnaIgraID");

                    b.HasIndex("AdministratorID");

                    b.HasIndex("KlijentID");

                    b.HasIndex("NagradaID");

                    b.ToTable("NagradnaIgra");
                });

            modelBuilder.Entity("WebApplication6.Models.PoslataNotifikacija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumSlanja");

                    b.Property<bool>("IsProcitano");

                    b.Property<int>("RezervacijaId");

                    b.HasKey("Id");

                    b.HasIndex("RezervacijaId");

                    b.ToTable("PoslataNotifikacija");
                });

            modelBuilder.Entity("WebApplication6.Models.Rezervacija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumRezervacije");

                    b.Property<int>("KlijentId");

                    b.Property<int>("TerminId");

                    b.Property<int>("TretmanId");

                    b.HasKey("Id");

                    b.HasIndex("KlijentId");

                    b.HasIndex("TerminId");

                    b.HasIndex("TretmanId");

                    b.ToTable("Rezervacija");
                });

            modelBuilder.Entity("WebApplication6.Models.Termin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Rezervisan");

                    b.Property<int>("UposlenikId");

                    b.Property<DateTime>("VrijemeTermina");

                    b.HasKey("Id");

                    b.HasIndex("UposlenikId");

                    b.ToTable("Termin");
                });

            modelBuilder.Entity("WebApplication6.Models.Tretman", b =>
                {
                    b.Property<int>("TretmanID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cijena");

                    b.Property<string>("Naziv")
                        .IsRequired();

                    b.HasKey("TretmanID");

                    b.ToTable("Tretman");
                });

            modelBuilder.Entity("WebApplication6.Models.Uposlenik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .IsRequired();

                    b.Property<string>("BrojTelefona")
                        .IsRequired();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<DateTime>("DatumZaposljenja");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Imeprezime")
                        .IsRequired();

                    b.Property<int?>("KorisnickiNalogId");

                    b.HasKey("Id");

                    b.HasIndex("KorisnickiNalogId");

                    b.ToTable("Uposlenik");
                });

            modelBuilder.Entity("WebApplication6.Models.Admin", b =>
                {
                    b.HasOne("WebApplication6.Models.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId");
                });

            modelBuilder.Entity("WebApplication6.Models.AutorizacijskiToken", b =>
                {
                    b.HasOne("WebApplication6.Models.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication6.Models.Clanak", b =>
                {
                    b.HasOne("WebApplication6.Models.Admin", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication6.Models.ClanciKategorija", "ClanciKategorija")
                        .WithMany()
                        .HasForeignKey("ClanciKategorijaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication6.Models.Klijent", b =>
                {
                    b.HasOne("WebApplication6.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication6.Models.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId");
                });

            modelBuilder.Entity("WebApplication6.Models.NagradnaIgra", b =>
                {
                    b.HasOne("WebApplication6.Models.Admin", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication6.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication6.Models.Nagrada", "Nagrada")
                        .WithMany()
                        .HasForeignKey("NagradaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication6.Models.PoslataNotifikacija", b =>
                {
                    b.HasOne("WebApplication6.Models.Rezervacija", "rezervacija")
                        .WithMany()
                        .HasForeignKey("RezervacijaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication6.Models.Rezervacija", b =>
                {
                    b.HasOne("WebApplication6.Models.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication6.Models.Termin", "Termin")
                        .WithMany()
                        .HasForeignKey("TerminId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication6.Models.Tretman", "Tretman")
                        .WithMany()
                        .HasForeignKey("TretmanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication6.Models.Termin", b =>
                {
                    b.HasOne("WebApplication6.Models.Uposlenik", "Uposlenik")
                        .WithMany()
                        .HasForeignKey("UposlenikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication6.Models.Uposlenik", b =>
                {
                    b.HasOne("WebApplication6.Models.KorisnickiNalog", "KorisnickiNalog")
                        .WithMany()
                        .HasForeignKey("KorisnickiNalogId");
                });
#pragma warning restore 612, 618
        }
    }
}
