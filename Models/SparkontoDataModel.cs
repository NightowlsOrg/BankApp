using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class SparkontoDataModel
{
    [Key]
    public Guid SparkontoId { get; set; } // Unique identifier
    public Guid KundId { get; set; } // Assuming it's linked to a customer
    public decimal Saldo { get; set; } // Balance in the account

    // Link to KundDataModel if needed
    public KundDataModel Kund { get; set; } // Optional navigation property
}