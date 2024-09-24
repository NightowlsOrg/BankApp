namespace BankApp.Application;

// Skapar Interface-klassen IKundService
public interface IKundService
{
    // Hämta en kund från databasen med id
    Task<KundDTO?> GetKundByIdAsync(Guid kundId);
    
    // SKA BORT
    // Validera en kund med hjälp av förnamn och lösenord
    Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord);

    // Lägg till en kund i databasen
    Task AddKundAsync(KundDTO kundDto);

    // Uppdatera en kund i databasen
    Task UpdateKundAsync(KundDTO kundDto);

    // Ta bort en kund från databasen
    Task DeleteKundAsync(Guid kundId);

    // Lista alla kunder i databasen
    Task<IEnumerable<KundDTO>> GetAllKunderAsync();
}