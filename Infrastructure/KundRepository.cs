using BankApp.Domain;

namespace BankApp.Infrastructure;

// Implementerar Interface-klassen IKundRepository
// Registrera IKundRepository som en scoped service i Program.cs
// Infrastructure är tredje och sista stället att mocka kundobjektet
// Här ska databasen anslutas
public class KundRepository : IKundRepository
{
    public Kund GetKundById(int id)
    {
        // Finns ingen databas konfigurerad så mockas ett kundobjekt (om programmet startats med att läsa från en databas i Program.cs)
        return new Kund("1661-05-01", "Infrastructure", "Repository", "Gatan 1", "123 45", "Staden", "070-123 45 67", "epost@domain.se");   // Mock implementation
    }
}