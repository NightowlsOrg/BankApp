namespace BankApp.Domain;

// Skapar Interface-klassen IKundRepository
// Här ska CRUD-metoderna för Kund finnas genom implementation av ett Repository-mönster
public interface IKundRepository
{
    // Hämta en kund med hjälp av id
    Task<Kund?> GetByIdAsync(Guid id);

    // Lägg till en kund
    Task AddAsync(Kund kund);
    
    // Task UpdateAsync(Kund kund);
    
    // Task DeleteAsync(Guid id);

    // Lista alla kunder
    Task<IEnumerable<Kund?>> GetAllAsync();

    // Validera en kund
    Task<Kund?> ValidateKundAsync(string förnamn, string lösenord);
}