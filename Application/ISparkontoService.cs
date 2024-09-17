namespace BankApp.Application;

public interface ISparkontoService
{
    Task<SparkontoDTO> OpenSparkontoAsync(Guid kundId);
    // Task DepositAsync(Guid accountId, decimal amount);
    // Task WithdrawAsync(Guid accountId, decimal amount);
    // Task<decimal> GetBalanceAsync(Guid accountId);
    Task<IEnumerable<SparkontoDTO>> GetByKundIdAsync(Guid kundId);
}