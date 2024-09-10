namespace BankApp.Application;

// Skapar Interface-klassen IKundService
public interface IKundService
{
    // Hämta en kund från databasen med hjälp av id
    Task<KundDTO?> GetKundByIdAsync(Guid id);
    
    // Validera en kund med hjälp av förnamn och lösenord
    Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord);

    // AddKundAsync används för att lägga till en ny kund i databasen

    Task<KundDTO> AddKundAsync(KundDTO kund);
}


