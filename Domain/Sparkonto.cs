namespace BankApp.Domain;

public class Sparkonto
{
    public Guid SparkontoId { get; private set; }
    public Guid KundId { get; private set; }
    public decimal Saldo { get; private set; }

    public Sparkonto(Guid kundId, decimal initialSaldo)
    {
        SparkontoId = Guid.NewGuid();
        KundId = kundId;
        Saldo = initialSaldo;
    }

    // Method to credit money to the account
    public void Deposit(decimal amount)
    {
        if (amount < 0)
            throw new InvalidOperationException("Cannot deposit negative amounts.");
        Saldo += amount;
    }

    // TODO: Add other methods like Withdraw, BalanceEnquiry, etc.
}
