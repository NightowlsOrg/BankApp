   using BankApp.Domain;
    using BankApp.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
namespace BankApp.Infrastructure;

// Implementerar Interface-klassen IKundRepository
// Registrera IKundRepository som en scoped service i Program.cs
// Infrastructure är tredje och sista stället att mocka kundobjektet
// Här ska databasen anslutas
public class KundRepository : IKundRepository
{

    private readonly AppDbContext _context;

    public KundRepository(AppDbContext context)
    {
        _context = context;
    }

    public Kund GetKundById(Guid id)
    {
        // Finns ingen databas konfigurerad så mockas ett kundobjekt (om programmet startats med att läsa från en databas i Program.cs)
        return new Kund(id, "lösenord", "1661-05-01", "Infrastructure", "Repository", "Gatan 1", "123 45", "Staden", "070-123 45 67", "epost@domain.se");   // Mock implementation
    }

    public async Task<Kund?> GetByIdAsync(Guid id)
    {
        var dataModel = await _context.Kunder.FindAsync(id);
        return dataModel == null ? null : new Kund(dataModel.Id, dataModel.Lösenord, dataModel.Personnummer, dataModel.Förnamn, dataModel.Efternamn, dataModel.Adress, dataModel.Postnummer, dataModel.Postort, dataModel.Tele, dataModel.Epost);
    }

    public async Task<Kund?> ValidateKundAsync(Kund kund)
    {
        var dataModel = await _context.Kunder.FirstOrDefaultAsync(k => k.Förnamn == kund.Förnamn && k.Lösenord == kund.Lösenord);
        return dataModel == null ? null : new Kund(dataModel.Id, dataModel.Lösenord, dataModel.Personnummer, dataModel.Förnamn, dataModel.Efternamn, dataModel.Adress, dataModel.Postnummer, dataModel.Postort, dataModel.Tele, dataModel.Epost);
    }

    public async Task<Kund?> AddAsync(Kund kund)
    {
        var dataModel = new KundDataModel
        {
            Id = kund.Id,
            Lösenord = kund.Lösenord,
            Personnummer = kund.Personnummer,            Förnamn = kund.Förnamn,
            Efternamn = kund.Efternamn,
            Adress = kund.Adress,
            Postnummer = kund.Postnummer,
            Postort = kund.Postort,
            Tele = kund.Tele,
            Epost = kund.Epost
        };

        _context.Kunder.Add(dataModel);
        await _context.SaveChangesAsync();

        return new Kund(dataModel.Id, dataModel.Lösenord, dataModel.Personnummer, dataModel.Förnamn, dataModel.Efternamn, dataModel.Adress, dataModel.Postnummer, dataModel.Postort, dataModel.Tele, dataModel.Epost);
    }
}






