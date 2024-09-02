namespace BankApp.Domain;

// I Domain ska objekten upprätthålla sin egen integritet genom att applicera affärsregler (t.ex. RegEx) på set-accessorn.
public class Kund
{
    public string Personnummer { get; set; }
    public string Förnamn { get; set; }
    public string Efternamn { get; set; }
    public string Adress { get; set; }
    public string Postnummer { get; set; }
    public string Postort { get; set; }
    public string Tele { get; set; }
    public string Epost { get; set; }

    public Kund(string personnummer, string förnamn, string efternamn, string adress, string postnummer, string postort, string tele, string epost)
    {
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