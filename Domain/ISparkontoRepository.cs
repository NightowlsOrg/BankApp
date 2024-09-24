namespace BankApp.Domain;

public interface ISparkontoRepository
{  
    // Hämta en kunds sparkonto
    Task<Sparkonto?> GetByKundIdAsync(Guid kundId);
    
    // Hämta ett sparkonto
    Task<Sparkonto?> GetByIdAsync(Guid sparkontoId);

    // Lägg till ett sparkonto
    Task AddAsync(Sparkonto sparkonto);

    // Hämta saldo för sparkonto
    Task<decimal> GetSaldoAsync(Guid sparkontoId);

    // Insättning på sparkonto
    Task DepositAsync(Guid sparkontoId, decimal belopp);

    // Uttag från sparkonto
    Task UttagAsync(Guid sparkontoId, decimal belopp);

    // Uppdatera sparkonto
    Task UpdateAsync(Sparkonto sparkonto);
}