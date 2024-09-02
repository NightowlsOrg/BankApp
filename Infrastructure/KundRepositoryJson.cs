using BankApp.Domain;
using System.Text.Json;

namespace BankApp.Infrastructure;

public class KundRepositoryJson : IKundRepository
{
    private readonly string _filePath = "kunder.json";

    public Kund GetKundById(int id)
    {
        var kunder = ReadFromFile();
        var kundData = kunder.Find(k => k.Id == id);

        return kundData != null ? new Kund(kundData.Personnummer, kundData.Förnamn, kundData.Efternamn, kundData.Adress, kundData.Postnummer, kundData.Postort, kundData.Tele, kundData.Epost) : null;
    }

    private List<KundEntityJson> ReadFromFile()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<KundEntityJson>>(json);
        }

        return new List<KundEntityJson>();
    }
}