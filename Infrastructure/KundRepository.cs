using BankApp.Domain;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Infrastructure;

// Repository pattern
// Implementerar Interface-klassen IKundRepository
// Registrera IKundRepository som en scoped service i Dependency Injection-containern (Program.cs)
public class KundRepository : IKundRepository
{
    private readonly AppDbContext _context;

    // Kan göras om till primary constructor
    public KundRepository(AppDbContext context)
    {
        _context = context;
    }

    // Hämta en kund med id
    public async Task<Kund?> GetByIdAsync(Guid kundId)
    {
        var dataModel = await _context.Kunder.FindAsync(kundId);
        return dataModel == null ? null : new Kund(
            dataModel.KundId,
            dataModel.IsAdmin,
            dataModel.Lösenord,
            dataModel.Personnummer,
            dataModel.Förnamn,
            dataModel.Efternamn,
            dataModel.Adress,
            dataModel.Postnummer,
            dataModel.Postort,
            dataModel.Tele,
            dataModel.Epost);
    }

    // SKA BORT
    // Validera en kund med hjälp av förnamn och lösenord
    public async Task<Kund?> ValidateKundAsync(string förnamn, string lösenord)
    {
        var dataModel = await _context.Kunder.FirstOrDefaultAsync(k => k.Förnamn == förnamn && k.Lösenord == lösenord);
        return dataModel == null ? null : new Kund(
            dataModel.KundId,
            dataModel.IsAdmin,
            dataModel.Lösenord,
            dataModel.Personnummer,
            dataModel.Förnamn,
            dataModel.Efternamn,
            dataModel.Adress,
            dataModel.Postnummer,
            dataModel.Postort,
            dataModel.Tele,
            dataModel.Epost);
    }

    // Lägg till en kund
    public async Task AddAsync(Kund kund)
    {
        var dataModel = new KundDataModel
        {
            KundId = kund.KundId,
            IsAdmin = kund.IsAdmin,
            Lösenord = kund.Lösenord,
            Personnummer = kund.Personnummer,
            Förnamn = kund.Förnamn,
            Efternamn = kund.Efternamn,
            Adress = kund.Adress,
            Postnummer = kund.Postnummer,
            Postort = kund.Postort,
            Tele = kund.Tele,
            Epost = kund.Epost
        };

        _context.Kunder.Add(dataModel);
        await _context.SaveChangesAsync();
    }

    // Lista alla kunder
    public async Task<IEnumerable<Kund>> GetAllAsync()
    {
        return await _context.Kunder.Select(k => new Kund(
            k.KundId,
            k.IsAdmin,
            k.Lösenord,
            k.Personnummer,
            k.Förnamn,
            k.Efternamn,
            k.Adress,
            k.Postnummer,
            k.Postort,
            k.Tele,
            k.Epost)).ToListAsync();
    }

    // Uppdatera en kund
    public async Task UpdateAsync(Kund kund)
    {
        var dataModel = await _context.Kunder.FindAsync(kund.KundId);
        if (dataModel != null)
        {
            dataModel.IsAdmin = kund.IsAdmin;
            dataModel.Personnummer = kund.Personnummer;
            dataModel.Förnamn = kund.Förnamn;
            dataModel.Efternamn = kund.Efternamn;
            dataModel.Adress = kund.Adress;
            dataModel.Postnummer = kund.Postnummer;
            dataModel.Postort = kund.Postort;
            dataModel.Tele = kund.Tele;
            dataModel.Epost = kund.Epost;
            dataModel.Lösenord = kund.Lösenord;
            await _context.SaveChangesAsync();
        }
    }

    // Ta bort en kund
    public async Task DeleteAsync(Guid kundId)
    {
        var dataModel = await _context.Kunder.FindAsync(kundId);
        if (dataModel != null)
        {
            _context.Kunder.Remove(dataModel);
            await _context.SaveChangesAsync();
        }
    }
}