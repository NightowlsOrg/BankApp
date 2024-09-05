namespace BankApp.Domain
{
    public class Kund
    {
        public Guid Id { get; private set; }
        private string _lösenord;
        public string Lösenord
        {
            get => _lösenord;
            private set
            {
                // Add validation for the password here
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Lösenord cannot be empty.");
                }
                _lösenord = value;
            }
        }

        public string Personnummer { get; private set; } = string.Empty;
        public string Förnamn { get; private set; } = string.Empty;
        public string Efternamn { get; private set; } = string.Empty;
        public string Adress { get; private set; } = string.Empty;
        public string Postnummer { get; private set; } = string.Empty;
        public string Postort { get; private set; } = string.Empty;
        public string Tele { get; private set; } = string.Empty;
        public string Epost { get; private set; } = string.Empty;

        public Kund(Guid id, string lösenord, string personnummer, string förnamn, string efternamn, string adress, string postnummer, string postort, string tele, string epost)
        {
            Id = id;
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
}
