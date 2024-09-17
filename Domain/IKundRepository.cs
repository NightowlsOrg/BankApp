namespace BankApp.Domain;

// Skapar Interface-klassen IKundRepository
// Här ska CRUD-metoderna för Kund finnas genom implementation av ett Repository-mönster
public interface IKundRepository
{
    // Hämta en kund med hjälp av id
    Task<Kund?> GetByIdAsync(Guid kundId);

    // Lägg till en kund
    Task AddAsync(Kund kund);
    
    // Uppdatera en kund
    Task UpdateAsync(Kund kund);
    
    // Ta bort en kund
    Task DeleteAsync(Guid kundId);

    // Lista alla kunder
    Task<IEnumerable<Kund>> GetAllAsync();

    // Validera en kund
    Task<Kund?> ValidateKundAsync(string förnamn, string lösenord);
}