namespace BankApp.Application;

public interface ISparkontoService
{
    Task<SparkontoDTO> OpenSparkontoAsync(Guid kundId);
    // Add other methods as needed (like FetchAccounts, etc.)

    // Add the following methods to the interface
    // Copy the method signatures from the SavingsAccountService class
    // Copilot will help you with this
    // Task DepositAsync(Guid accountId, decimal amount);
    // Task WithdrawAsync(Guid accountId, decimal amount);
    // Task<decimal> GetBalanceAsync(Guid accountId);
    Task<IEnumerable<SparkontoDTO>> GetByKundIdAsync(Guid kundId);
}