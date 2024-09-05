namespace BankApp.Application
{
    using BankApp.Domain;
    using BankApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class KundService : IKundService
    {
        private readonly IKundRepository _kundRepository;

        public KundService(IKundRepository kundRepository)
        {
            _kundRepository = kundRepository;
        }

        public async Task<KundDTO?> GetByIdAsync(Guid id)
        {
            var kund = await _kundRepository.GetByIdAsync(id);
            return kund == null ? null : new KundDTO
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
        }

        public async Task<KundDTO?> GetAllAsync()
        {
            var kunder = await _kundRepository.GetAllAsync();
            // Example logic: return the first customer or null if none
            var firstKund = kunder.FirstOrDefault();
            return firstKund == null ? null : new KundDTO
            {
                Id = firstKund.Id,
                Lösenord = firstKund.Lösenord,
                Personnummer = firstKund.Personnummer,
                Förnamn = firstKund.Förnamn,
                Efternamn = firstKund.Efternamn,
                Adress = firstKund.Adress,
                Postnummer = firstKund.Postnummer,
                Postort = firstKund.Postort,
                Tele = firstKund.Tele,
                Epost = firstKund.Epost
            };
        }

        public async Task<KundDTO> AddKundAsync(KundDTO kund)
        {
            var newKund = new Kund(Guid.NewGuid(), kund.Lösenord, kund.Personnummer, kund.Förnamn, kund.Efternamn, kund.Adress, kund.Postnummer, kund.Postort, kund.Tele, kund.Epost);
            await _kundRepository.AddAsync(newKund);
            return new KundDTO
            {
                Id = newKund.Id,
                Lösenord = newKund.Lösenord,
                Personnummer = newKund.Personnummer,
                Förnamn = newKund.Förnamn,
                Efternamn = newKund.Efternamn,
                Adress = newKund.Adress,
                Postnummer = newKund.Postnummer,
                Postort = newKund.Postort,
                Tele = newKund.Tele,
                Epost = newKund.Epost
            };
        }

        public async Task<KundDTO?> ValidateKundAsync(string förnamn, string lösenord)
        {
            var kund = await _kundRepository.ValidateKundAsync(förnamn, lösenord);
            return kund == null ? null : new KundDTO
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
        }
    }
}
