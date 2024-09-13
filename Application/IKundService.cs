namespace BankApp.Application;

// Skapar Interface-klassen IKundService
public interface IKundService
{
    // Hämta en kund från databasen med hjälp av id
    Task<KundDTO?> GetKundByIdAsync(Guid id);
    
    // Validera en kund med hjälp av förnamn och lösenord
    Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord);

    // Lägg till en kund i databasen
    Task AddKundAsync(KundDTO kund);

    // Uppdatera en kund i databasen
    Task UpdateKundAsync(KundDTO kund);

    // Ta bort en kund från databasen
    Task DeleteKundAsync(Guid id);

    // Lista alla kunder i databasen
    Task<IEnumerable<KundDTO>> GetAllKunderAsync();
}