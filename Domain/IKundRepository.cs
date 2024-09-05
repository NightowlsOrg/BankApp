namespace BankApp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IKundRepository
    {
        Task<Kund?> GetByIdAsync(Guid id);
        Task<IEnumerable<Kund>> GetAllAsync();
        Task AddAsync(Kund kund);
        Task<Kund?> ValidateKundAsync(string förnamn, string lösenord);
    }
}
