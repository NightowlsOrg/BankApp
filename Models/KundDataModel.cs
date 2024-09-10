using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class KundDataModel
{
    public Guid Id { get; set; }

    [Required]
    public string Lösenord { get; set; }

    [Required]
    public string Personnummer { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Använd endast bokstäver")]
    public string Förnamn { get; set; } = string.Empty;

    [Required]
    public string Efternamn { get; set; } = string.Empty;

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