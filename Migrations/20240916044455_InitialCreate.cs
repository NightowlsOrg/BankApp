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
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
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
                columns: new[] { "Id", "Adress", "Efternamn", "Epost", "Förnamn", "IsAdmin", "Lösenord", "Personnummer", "Postnummer", "Postort", "Tele" },
                values: new object[,]
                {
                    { new Guid("6018c2ba-99b6-4c8f-8caa-7c240b7c6e47"), "Testgatan 1", "Testare", "test.testare@testby.se", "Test", false, "pass", "1111-11-11", "111 11", "Testby", "111-111 11 11" },
                    { new Guid("cd7677b3-0f9b-4a90-95dc-fb2289adfdfb"), "Knasgatan 1", "Knasare", "knaspelle.knasare@knas.se", "Knaspelle", false, "knaspass", "1977-04-25", "123 45", "Knasby", "070-123 45 67" },
                    { new Guid("e942715d-6de8-4289-91c4-fe13f5a7588a"), "Ankgatan 1", "Ankare", "ankpelle.ankare@ank.se", "Ankpelle", false, "ankpass", "2011-09-11", "543 21", "Ankby", "070-765 43 21" }
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
