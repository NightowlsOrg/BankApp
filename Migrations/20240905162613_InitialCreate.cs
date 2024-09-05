using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Lösenord = table.Column<string>(type: "TEXT", nullable: false),
                    Personnummer = table.Column<string>(type: "TEXT", nullable: false),
                    Förnamn = table.Column<string>(type: "TEXT", nullable: false),
                    Efternamn = table.Column<string>(type: "TEXT", nullable: false),
                    Adress = table.Column<string>(type: "TEXT", nullable: false),
                    Postnummer = table.Column<string>(type: "TEXT", nullable: false),
                    Postort = table.Column<string>(type: "TEXT", nullable: false),
                    Tele = table.Column<string>(type: "TEXT", nullable: false),
                    Epost = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Kunder",
                columns: new[] { "Id", "Adress", "Efternamn", "Epost", "Förnamn", "Lösenord", "Personnummer", "Postnummer", "Postort", "Tele" },
                values: new object[,]
                {
                    { new Guid("3d3f73f7-b3d4-4315-9224-531684e9e86c"), "Ankgatan 1", "Ankare", "ankpelle.ankare@ank.se", "Ankpelle", "ankpass", "2011-09-11", "543 21", "Ankby", "070-765 43 21" },
                    { new Guid("495c8803-cf2d-4cb6-be0b-d157fc22bd44"), "Testgatan 1", "Testare", "test.testare@testby.se", "Test", "pass", "1111-11-11", "111 11", "Testby", "111-111 11 11" },
                    { new Guid("a6ad1d69-bd29-4a5d-97bf-e5614cd8cd28"), "Knasgatan 1", "Knasare", "knaspelle.knasare@knas.se", "Knaspelle", "knaspass", "1977-04-25", "123 45", "Knasby", "070-123 45 67" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}
