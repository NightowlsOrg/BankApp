using System.Text.Json.Serialization;

namespace BankApp.Infrastructure;

// Mappar innehållet i JSON-filen kunder.json till klassen KundEntityJson
public class KundEntityJson
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("personnummer")]
    public string? Personnummer { get; set; }

    [JsonPropertyName("förnamn")]
    public string? Förnamn { get; set; }

    [JsonPropertyName("efternamn")]
    public string? Efternamn { get; set; }

    [JsonPropertyName("adress")]
    public string? Adress { get; set; }

    [JsonPropertyName("postnummer")]
    public string? Postnummer { get; set; }

    [JsonPropertyName("postort")]
    public string? Postort { get; set; }

    [JsonPropertyName("tele")]
    public string? Tele { get; set; }

    [JsonPropertyName("epost")]
    public string? Epost { get; set; }
}