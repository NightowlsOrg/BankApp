namespace BankApp.Models;

public class SparkontoDataModel
{
    public Guid Id { get; set; } // Unique identifier
    public Guid KundId { get; set; } // Assuming it's linked to a customer
    public decimal Saldo { get; set; } // Balance in the account

    // Link to KundDataModel if needed
    public KundDataModel Kund { get; set; } // Optional navigation property
}