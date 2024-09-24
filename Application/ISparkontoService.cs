namespace BankApp.Application;

public interface ISparkontoService
{
    // Skapa sparkonto för kund
    Task CreateSparkontoAsync(Guid kundId, SparkontoDTO sparkontoDto);

    // Hämta sparkonto för kund
    Task<SparkontoDTO?> GetSparkontoByKundIdAsync(Guid kundId);
    
    // Hämta sparkonto med id
    Task<SparkontoDTO?> GetSparkontoByIdAsync(Guid sparkontoId);
    
    // Hämta saldo för sparkonto
    Task<decimal> GetSaldoAsync(Guid sparkontoId);

    // Insättning på sparkonto
    Task InsattningAsync(Guid sparkontoId, decimal belopp);

    // Uttag från sparkonto
    Task UttagAsync(Guid sparkontoId, decimal belopp);
}