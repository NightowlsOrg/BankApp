namespace BankApp.Application;

// Skapar Interface-klassen IKundService
public interface IKundService
{
    Task<KundDTO?> GetKundByIdAsync(Guid id);
    
    // ValidateKundAsync används för att validera om kunden finns i databasen
    Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord);
}