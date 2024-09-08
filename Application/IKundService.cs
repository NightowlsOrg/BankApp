namespace BankApp.Application;

// Skapar Interface-klassen IKundService
public interface IKundService
{
    Task<KundDTO?> GetKundByIdAsync(Guid id);
    
    // ValidateKundAsync används för att validera om kunden finns i databasen
    Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord);

    // AddKundAsync används för att lägga till en ny kund i databasen

    Task<KundDTO> AddKundAsync(KundDTO kund);
}


