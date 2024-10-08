using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class KundDataModel
{
    [Key]
    public Guid KundId { get; set; }

    [Required]
    public bool IsAdmin { get; set; }

    [Required]
    public string Lösenord { get; set; } // SKA BORT

    [Required]
    public string Personnummer { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Använd endast bokstäver.")]
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
    public string? Epost { get; set; } = string.Empty;

    // Navigations property för att koppla ihop KundDataModel med SparkontoDataModel
    public SparkontoDataModel? Sparkonto { get; set; }
}