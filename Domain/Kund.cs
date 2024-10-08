namespace BankApp.Domain;

// I Domain ska objekten upprätthålla sin egen integritet genom att applicera affärsregler (t.ex. RegEx) på set-accessorn.
public class Kund
{
    public Guid KundId { get; private set; }
    public bool IsAdmin { get; private set; }
    public string Lösenord { get; private set; }
    public string Personnummer { get; set; }
    public string Förnamn { get; set; }
    public string Efternamn { get; set; }
    public string Adress { get; set; }
    public string Postnummer { get; set; }
    public string Postort { get; set; }
    public string Tele { get; set; }
    public string Epost { get; private set; }

    // Kan göras om till primary constructor
    public Kund(Guid kundId, bool isAdmin, string lösenord ,string personnummer, string förnamn, string efternamn, string adress, string postnummer, string postort, string tele, string epost)
    {
        KundId = kundId;
        IsAdmin = isAdmin;
        Lösenord = lösenord;
        Personnummer = personnummer;
        Förnamn = förnamn;
        Efternamn = efternamn;
        Adress = adress;
        Postnummer = postnummer;
        Postort = postort;
        Tele = tele;
        Epost = epost;
    }
}