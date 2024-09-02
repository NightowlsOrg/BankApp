using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class Kund
{
    [Required]
    public string? Personnummer { get; set; }
    
    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Använd endast bokstäver, inga siffror eller specialtecken.")]
    public string? Förnamn { get; set; }

    [Required]
    public string? Efternamn { get; set; }

    [Required]
    public string? Adress { get; set; }

    [Required]
    public string? Postnummer { get; set; }

    [Required]
    public string? Postort { get; set; }

    [Required]
    public string? Tele { get; set; }

    [Required]
    public string? Epost { get; set; }
}