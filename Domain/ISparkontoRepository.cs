namespace BankApp.Domain;

public interface ISparkontoRepository
{
    Task<Sparkonto?> GetByIdAsync(Guid id);
    Task AddAsync(Sparkonto sparkonto);
    Task<IEnumerable<Sparkonto>> GetByKundIdAsync(Guid kundId);
}