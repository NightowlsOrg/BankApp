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
                    KundId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    table.PrimaryKey("PK_Kunder", x => x.KundId);
                });

            migrationBuilder.CreateTable(
                name: "Sparkonton",
                columns: table => new
                {
                    SparkontoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    KundId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Saldo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sparkonton", x => x.SparkontoId);
                    table.ForeignKey(
                        name: "FK_Sparkonton_Kunder_KundId",
                        column: x => x.KundId,
                        principalTable: "Kunder",
                        principalColumn: "KundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Kunder",
                columns: new[] { "KundId", "Adress", "Efternamn", "Epost", "Förnamn", "IsAdmin", "Lösenord", "Personnummer", "Postnummer", "Postort", "Tele" },
                values: new object[,]
                {
                    { new Guid("004c07d8-8f3d-49fc-9766-8cedcd291c13"), "Knasgatan 1", "Knasare", "knaspelle.knasare@knas.se", "Knaspelle", false, "knaspass", "1977-04-25", "123 45", "Knasby", "070-123 45 67" },
                    { new Guid("057c0933-21d1-405a-8292-c8801fb09092"), "Testgatan 1", "Testare", "test.testare@testby.se", "Test", false, "pass", "1111-11-11", "111 11", "Testby", "111-111 11 11" },
                    { new Guid("f5b54980-14e0-4ab4-af96-a861a1ce5c56"), "Ankgatan 1", "Ankare", "ankpelle.ankare@ank.se", "Ankpelle", false, "ankpass", "2011-09-11", "543 21", "Ankby", "070-765 43 21" }
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
