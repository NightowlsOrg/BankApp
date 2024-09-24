using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class SparkontoViewModel
{
    [Key]
    public Guid SparkontoId { get; set; }

    public Guid KundId { get; set; }

    public bool HasSparkonto { get; set; }
    
    public decimal Saldo { get; set; }

    public decimal? Belopp { get; set; }

    // Länk till KundDataModel
    // Virtual används för lazy loading
    public virtual KundDataModel Kund { get; set; }
}