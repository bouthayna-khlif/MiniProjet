using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProjetBibliotheque.Migrations
{
    public partial class SupprAdmins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdmin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mdp_Admin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomAdmin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrenomAdmin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelephoneAdmin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });
        }
    }
}
