namespace BankApp.Application;

// Flyttar data från Application till Presentation (Data Transfer Object)
public class KundDTO
{
    public Guid Id { get; set; }
    public string Lösenord { get; set; }
    public string Personnummer { get; set; } = string.Empty;
    public string Förnamn { get; set; } = string.Empty;
    public string Efternamn { get; set; } = string.Empty;
    public string? Adress { get; set; }
    public string? Postnummer { get; set; }
    public string? Postort { get; set; }
    public string? Tele { get; set; }
    public string? Epost { get; set; }
}