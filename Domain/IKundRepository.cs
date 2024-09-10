namespace BankApp.Domain;

// Skapar Interface-klassen IKundRepository
// Här ska CRUD-metoderna för Kund finnas genom implementation av ett Repository-mönster
public interface IKundRepository
{
    // Hämta en kund med hjälp av id
    Task<Kund?> GetByIdAsync(Guid id);

    // Task<IEnumerable<Kund>> GetAllAsync();
    
    // Task AddAsync(Kund kund);
    
    // Task UpdateAsync(Kund kund);
    
    // Task DeleteAsync(Guid id);

    Task<Kund?> ValidateKundAsync(string förnamn, string lösenord);

    Task<Kund?> AddAsync(Kund kund);
}
