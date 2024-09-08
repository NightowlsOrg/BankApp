namespace BankApp.Application;

// Flyttar data från Application till Presentation (Data Transfer Object)
public class KundDTO
{
    public Guid Id { get; set; }
    public string Lösenord { get; set; } 
    public string Personnummer { get; set; } 
    public string Förnamn { get; set; } 
    public string Efternamn { get; set; } 
    public string Adress { get; set; } 
    public string Postnummer { get; set; } 
    public string Postort { get; set; } 
    public string Tele { get; set; } 
    public string Epost { get; set; } 
}