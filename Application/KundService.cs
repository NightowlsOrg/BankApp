using BankApp.Domain;

namespace BankApp.Application;

// Implementerar Interface-klassen IKundService
// Registrera IKundService som en scoped service i Program.cs
public class KundService : IKundService
{
     private readonly IKundRepository _kundRepository;

    // Ärver in en instans av IKundRepository som är definierad i Domain
    public KundService(IKundRepository kundRepository)
    {
        _kundRepository = kundRepository;
    }
 
   public async Task<KundDTO?> GetKundByIdAsync(Guid id)
    {
        var kund = await _kundRepository.GetByIdAsync(id);
        return kund == null ? null : new KundDTO { Id = kund.Id, Lösenord = kund.Lösenord, Personnummer = kund.Personnummer, Förnamn = kund.Förnamn, Efternamn = kund.Efternamn, Adress = kund.Adress, Postnummer = kund.Postnummer, Postort = kund.Postort, Tele = kund.Tele, Epost = kund.Epost };
    }

    // ValidateKundAsync används för att validera om kunden finns i databasen
    public async Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord)
    {
        var kund = new Kund(Guid.Empty, lösenord, "", förnamn, "", "", "", "", "", "");
        var validatedKund = await _kundRepository.ValidateKundAsync(kund);
        return validatedKund == null ? null : new KundDTO { Id = validatedKund.Id, Lösenord = validatedKund.Lösenord, Personnummer = validatedKund.Personnummer, Förnamn = validatedKund.Förnamn, Efternamn = validatedKund.Efternamn, Adress = validatedKund.Adress, Postnummer = validatedKund.Postnummer, Postort = validatedKund.Postort, Tele = validatedKund.Tele, Epost = validatedKund.Epost };
    }
}