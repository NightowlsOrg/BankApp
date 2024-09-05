namespace BankApp.Application
{
    using System;
    using System.Threading.Tasks;
    using BankApp.Models;

public interface IKundService
{
    Task<KundDTO?> GetByIdAsync(Guid id);
    Task<KundDTO?> GetAllAsync();
    Task<KundDTO> AddKundAsync(KundDTO kund);
    Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord);
}
}
