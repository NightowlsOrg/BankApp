using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class KundViewModel
{
    public Guid KundId { get; set; }
    public bool IsAdmin { get; set; }
    public string Lösenord { get; set; } = string.Empty;    // SKA BORT
    public string Personnummer { get; set; } = string.Empty;
    public string Förnamn { get; set; } = string.Empty;
    public string Efternamn { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public string Postnummer { get; set; } = string.Empty;
    public string Postort { get; set; } = string.Empty;
    public string Tele { get; set; } = string.Empty;
    public string Epost { get; set; } = string.Empty;
    public decimal Saldo { get; set; } = -1;    // SKA BORT
}