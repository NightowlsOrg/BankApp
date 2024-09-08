namespace BankApp.Domain;

// Skapar Interface-klassen IKundRepository
// Här ska CRUD-metoderna för Kund finnas genom implementation av ett Repository-mönster
public interface IKundRepository
{
    Task<Kund?> GetByIdAsync(Guid id);

    // Task<IEnumerable<Kund>> GetAllAsync();
    
    // Task AddAsync(Kund kund);
    
    // Task UpdateAsync(Kund kund);
    
    // Task DeleteAsync(Guid id);

    Task<Kund?> ValidateKundAsync(Kund kund);

    Task<Kund?> AddAsync(Kund kund);
}












