namespace BankApp.Infrastructure
{
    using BankApp.Domain;
    using BankApp.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class KundRepository : IKundRepository
    {
        private readonly AppDbContext _context;

        public KundRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Kund?> GetByIdAsync(Guid id)
        {
            var dataModel = await _context.Kunder
                .FirstOrDefaultAsync(k => k.Id == id);

            return dataModel == null ? null : new Kund(
                dataModel.Id,
                dataModel.Lösenord,
                dataModel.Personnummer,
                dataModel.Förnamn,
                dataModel.Efternamn,
                dataModel.Adress,
                dataModel.Postnummer,
                dataModel.Postort,
                dataModel.Tele,
                dataModel.Epost
            );
        }

        public async Task<IEnumerable<Kund>> GetAllAsync()
        {
            var dataModels = await _context.Kunder.ToListAsync();

            return dataModels.Select(dataModel => new Kund(
                dataModel.Id,
                dataModel.Lösenord,
                dataModel.Personnummer,
                dataModel.Förnamn,
                dataModel.Efternamn,
                dataModel.Adress,
                dataModel.Postnummer,
                dataModel.Postort,
                dataModel.Tele,
                dataModel.Epost
            ));
        }

        public async Task AddAsync(Kund kund)
        {
            var dataModel = new KundDataModel
            {
                Id = kund.Id,
                Lösenord = kund.Lösenord,
                Personnummer = kund.Personnummer,
                Förnamn = kund.Förnamn,
                Efternamn = kund.Efternamn,
                Adress = kund.Adress,
                Postnummer = kund.Postnummer,
                Postort = kund.Postort,
                Tele = kund.Tele,
                Epost = kund.Epost
            };

            _context.Kunder.Add(dataModel);
            await _context.SaveChangesAsync();
        }

        public async Task<Kund?> ValidateKundAsync(string förnamn, string lösenord)
        {
            var dataModel = await _context.Kunder
                .FirstOrDefaultAsync(k => k.Förnamn == förnamn && k.Lösenord == lösenord);

            return dataModel == null ? null : new Kund(
                dataModel.Id,
                dataModel.Lösenord,
                dataModel.Personnummer,
                dataModel.Förnamn,
                dataModel.Efternamn,
                dataModel.Adress,
                dataModel.Postnummer,
                dataModel.Postort,
                dataModel.Tele,
                dataModel.Epost
            );
        }
    }
}
