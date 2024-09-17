using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<KundDataModel> Kunder { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSet för SparkontoDataModel
    public DbSet<SparkontoDataModel> Sparkonton { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KundDataModel>().HasData(
            new KundDataModel { KundId = Guid.NewGuid(), Lösenord = "knaspass", Personnummer = "1977-04-25", Förnamn = "Knaspelle", Efternamn = "Knasare", Adress = "Knasgatan 1", Postnummer = "123 45", Postort = "Knasby", Tele = "070-123 45 67", Epost = "knaspelle.knasare@knas.se" },
            new KundDataModel { KundId = Guid.NewGuid(), Lösenord = "ankpass", Personnummer = "2011-09-11", Förnamn = "Ankpelle", Efternamn = "Ankare", Adress = "Ankgatan 1", Postnummer = "543 21", Postort = "Ankby", Tele = "070-765 43 21", Epost = "ankpelle.ankare@ank.se" },
            new KundDataModel { KundId = Guid.NewGuid(), Lösenord = "pass", Personnummer = "1111-11-11", Förnamn = "Test", Efternamn = "Testare", Adress = "Testgatan 1", Postnummer = "111 11", Postort = "Testby", Tele = "111-111 11 11", Epost = "test.testare@testby.se" }
        );

        modelBuilder.Entity<SparkontoDataModel>().HasData(
            // Seeds läggs här
        );
    }
}