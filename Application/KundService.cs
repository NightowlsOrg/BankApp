using BankApp.Domain;

namespace BankApp.Application;

// Implementerar Interface-klassen IKundService
// Registrera IKundService som en scoped service i Dependency Injection-containern (Program.cs)
public class KundService : IKundService
{
    private readonly IKundRepository _kundRepository;

    // Ärver in en instans av IKundRepository som är definierad i Domain
    // Kan göras om till primary constructor
    public KundService(IKundRepository kundRepository)
    {
        _kundRepository = kundRepository;
    }

    // Hämta en kund från databasen med id
    public async Task<KundDTO?> GetKundByIdAsync(Guid kundId)
    {
        var kund = await _kundRepository.GetByIdAsync(kundId);
        return kund == null ? null : new KundDTO {
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
    }

    // SKA BORT
    // Validera en kund med hjälp av förnamn och lösenord
    public async Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord)
    {
        var validatedKund = await _kundRepository.ValidateKundAsync(förnamn, lösenord);
        return validatedKund == null ? null : new KundDTO {
            KundId = validatedKund.KundId,
            IsAdmin = validatedKund.IsAdmin,
            Lösenord = validatedKund.Lösenord,
            Personnummer = validatedKund.Personnummer,
            Förnamn = validatedKund.Förnamn,
            Efternamn = validatedKund.Efternamn,
            Adress = validatedKund.Adress,
            Postnummer = validatedKund.Postnummer,
            Postort = validatedKund.Postort,
            Tele = validatedKund.Tele,
            Epost = validatedKund.Epost
        };
    }

    // Lägg till en kund i databasen
    public async Task AddKundAsync(KundDTO kundDto)
    {
        var kund = new Kund(
            Guid.NewGuid(),
            kundDto.IsAdmin,
            kundDto.Lösenord,
            kundDto.Personnummer,
            kundDto.Förnamn,
            kundDto.Efternamn,
            kundDto.Adress,
            kundDto.Postnummer,
            kundDto.Postort,
            kundDto.Tele,
            kundDto.Epost
        );
        await _kundRepository.AddAsync(kund);
    }

    // Lista alla kunder
    public async Task<IEnumerable<KundDTO>> GetAllKunderAsync()
    {
        return (await _kundRepository.GetAllAsync())
            .Select(kund => new KundDTO {
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
            });
    }

    // Uppdatera en kund i databasen
    public async Task UpdateKundAsync(KundDTO kundDto)
    {
        var kund = new Kund(
            kundDto.KundId,
            kundDto.IsAdmin,
            kundDto.Lösenord,
            kundDto.Personnummer,
            kundDto.Förnamn,
            kundDto.Efternamn,
            kundDto.Adress,
            kundDto.Postnummer,
            kundDto.Postort,
            kundDto.Tele,
            kundDto.Epost
        );
        await _kundRepository.UpdateAsync(kund);
    }

    public async Task DeleteKundAsync(Guid kundId)
    {
        await _kundRepository.DeleteAsync(kundId);
    }
}