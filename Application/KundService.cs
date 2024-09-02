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
 
    public KundDTO GetKund()
    {
        // Application är andra stället att mocka kundobjektet
        // Returnerar ett mockat KundDTO-objekt
        // return new KundDTO
        // {
        //     Personnummer = "1968-08-06",
        //     Förnamn = "Application",
        //     Efternamn = "KundService",
        //     Adress = "Gatan 1",
        //     Postnummer = "123 45",
        //     Postort = "Staden",
        //     Tele = "070-123 45 67",
        //     Epost = "epost@domain.se"
        // };

        // Hämta ett kund från Infrastructure repository
        var kund = _kundRepository.GetKundById(1);
        return new KundDTO
        {
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