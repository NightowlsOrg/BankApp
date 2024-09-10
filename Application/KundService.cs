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
 
    // Hämta en kund från databasen med hjälp av id
   public async Task<KundDTO?> GetKundByIdAsync(Guid id)
    {
        var kund = await _kundRepository.GetByIdAsync(id);
        return kund == null ? null : MapToKundDTO(kund);
    }

    // Validera en kund med hjälp av förnamn och lösenord
    public async Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord)
    {
        var validatedKund = await _kundRepository.ValidateKundAsync(förnamn, lösenord);
        return validatedKund == null ? null : MapToKundDTO(validatedKund);
    }

    // Lägg till en ny kund i databasen
    public async Task AddKundAsync(KundDTO kundDTO)
    {
        var kund = new Kund(
            Guid.NewGuid(),
            kundDTO.Lösenord,
            kundDTO.Personnummer,
            kundDTO.Förnamn,
            kundDTO.Efternamn,
            kundDTO.Adress,
            kundDTO.Postnummer,
            kundDTO.Postort,
            kundDTO.Tele,
            kundDTO.Epost
        );

        await _kundRepository.AddAsync(kund);
    }

    // Lista alla kunder
        public async Task<IEnumerable<KundDTO?>> GetAllAsync()
    {
        var kunder = await _kundRepository.GetAllAsync();
        return kunder.Select(MapToKundDTO); // Map returned entities to DTOs
    }

    // Mappa Kund-objekt till KundDTO-objekt (Data Transfer Object) för att skicka data mellan lager
    private KundDTO MapToKundDTO(Kund kund)
    {
        return new KundDTO
        {
            Id = kund.Id,
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
    }
}