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

            migrationBuilder.CreateTable(
                name: "Sparkonton",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    KundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Saldo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sparkonton", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sparkonton_Kunder_KundId",
                        column: x => x.KundId,
                        principalTable: "Kunder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kunder",
                columns: new[] { "Id", "Adress", "Efternamn", "Epost", "Förnamn", "IsAdmin", "Lösenord", "Personnummer", "Postnummer", "Postort", "Tele" },
                values: new object[,]
                {
                    { new Guid("77f40d45-9c10-4a99-91ce-f34782c3efe7"), "Ankgatan 1", "Ankare", "ankpelle.ankare@ank.se", "Ankpelle", false, "ankpass", "2011-09-11", "543 21", "Ankby", "070-765 43 21" },
                    { new Guid("ce9a3d12-96cf-428e-9ae7-b46d5509f2f9"), "Testgatan 1", "Testare", "test.testare@testby.se", "Test", false, "pass", "1111-11-11", "111 11", "Testby", "111-111 11 11" },
                    { new Guid("eeff997c-9a4d-49f3-9f66-9430a8db7025"), "Knasgatan 1", "Knasare", "knaspelle.knasare@knas.se", "Knaspelle", false, "knaspass", "1977-04-25", "123 45", "Knasby", "070-123 45 67" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sparkonton_KundId",
                table: "Sparkonton",
                column: "KundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sparkonton");

            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}
