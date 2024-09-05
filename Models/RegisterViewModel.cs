namespace BankApp.Models;

public class RegisterViewModel
{
    public int Id { get; set; }

    public string Personnummer { get; set; } = string.Empty;
    public string Förnamn { get; set; } = string.Empty;

    public string Efternamn { get; set; } = string.Empty;
    public string Lösenord { get; set; } = string.Empty;

    public string Adress { get; set; } = string.Empty;

    public string Postnummer { get; set; } = string.Empty;

    public string Postort { get; set; } = string.Empty;

    public string Tele { get; set; } = string.Empty;

    public string Epost { get; set; } = string.Empty;


}

