using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjetBibliotheque.Migrations
{
    public partial class CreateDbBibliotheque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    AuteurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomAuteur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrenomAuteur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailAuteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneAuteur = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.AuteurId);
                });

            migrationBuilder.CreateTable(
                name: "Domaines",
                columns: table => new
                {
                    DomaineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomDomaine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domaines", x => x.DomaineId);
                });

            migrationBuilder.CreateTable(
                name: "Lecteurs",
                columns: table => new
                {
                    LecteurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomLecteur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrenomLecteur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailLecteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneLecteur = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mdp_Lecteur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecteurs", x => x.LecteurId);
                });

            migrationBuilder.CreateTable(
                name: "Livres",
                columns: table => new
                {
                    LivreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombrePages = table.Column<int>(type: "int", nullable: false),
                    AuteurId = table.Column<int>(type: "int", nullable: false),
                    DomaineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livres", x => x.LivreId);
                    table.ForeignKey(
                        name: "FK_Livres_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "AuteurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livres_Domaines_DomaineId",
                        column: x => x.DomaineId,
                        principalTable: "Domaines",
                        principalColumn: "DomaineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emprunts",
                columns: table => new
                {
                    EmpruntId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LecteurId = table.Column<int>(type: "int", nullable: false),
                    LivreId = table.Column<int>(type: "int", nullable: false),
                    DateEmprunt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetour = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunts", x => x.EmpruntId);
                    table.ForeignKey(
                        name: "FK_Emprunts_Lecteurs_LecteurId",
                        column: x => x.LecteurId,
                        principalTable: "Lecteurs",
                        principalColumn: "LecteurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprunts_Livres_LivreId",
                        column: x => x.LivreId,
                        principalTable: "Livres",
                        principalColumn: "LivreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_LecteurId",
                table: "Emprunts",
                column: "LecteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_LivreId",
                table: "Emprunts",
                column: "LivreId");

            migrationBuilder.CreateIndex(
                name: "IX_Livres_AuteurId",
                table: "Livres",
                column: "AuteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Livres_DomaineId",
                table: "Livres",
                column: "DomaineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprunts");

            migrationBuilder.DropTable(
                name: "Lecteurs");

            migrationBuilder.DropTable(
                name: "Livres");

            migrationBuilder.DropTable(
                name: "Auteurs");

            migrationBuilder.DropTable(
                name: "Domaines");
        }
    }
}
