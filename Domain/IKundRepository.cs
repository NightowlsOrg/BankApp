namespace BankApp.Domain;

// Skapar Interface-klassen IKundRepository
// Här finns CRUD-metoderna för Kund
public interface IKundRepository
{
    // Hämta en kund med id
    Task<Kund?> GetByIdAsync(Guid kundId);

    // Lägg till en kund
    Task AddAsync(Kund kund);

    // Uppdatera en kund
    Task UpdateAsync(Kund kund);

    // Ta bort en kund
    Task DeleteAsync(Guid kundId);

    // Lista alla kunder
    Task<IEnumerable<Kund>> GetAllAsync();

    // SKA BORT
    // Validera en kund
    Task<Kund?> ValidateKundAsync(string förnamn, string lösenord);
}