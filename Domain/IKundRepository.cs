namespace BankApp.Domain;

// Skapar Interface-klassen IKundRepository
// Här ska CRUD-metoderna för Kund finnas genom implementation av ett Repository-mönster
public interface IKundRepository
{
    Kund GetKundById(int id);
}