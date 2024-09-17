namespace BankApp.Application;

public interface ISparkontoService
{
    Task<SparkontoDTO> OpenSparkontoAsync(Guid kundId);
    // Task DepositAsync(Guid sparkontoId, decimal amount);
    // Task WithdrawAsync(Guid sparkontoId, decimal amount);
    // Task<decimal> GetSaldoAsync(Guid sparkontoId);
    Task<IEnumerable<SparkontoDTO>> GetByKundIdAsync(Guid kundId);
}