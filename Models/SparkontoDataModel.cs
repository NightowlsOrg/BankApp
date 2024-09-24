using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class SparkontoDataModel

{
    [Key]
    public Guid SparkontoId { get; set; }

    public Guid KundId { get; set; }
    
    public decimal Saldo { get; set; }

    // Länk till KundDataModel
    // Virtual används för lazy loading
    public virtual KundDataModel Kund { get; set; }
}