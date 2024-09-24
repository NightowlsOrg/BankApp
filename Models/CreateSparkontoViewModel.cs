namespace BankApp.Models;

public class CreateSparkontoViewModel
{
    public Guid KundId { get; set; }
    public Guid SparkontoId { get; set; }
    public decimal Saldo { get; set; }
}