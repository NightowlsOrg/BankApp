using BankApp.Domain;
using Newtonsoft.Json; // Make sure to install Newtonsoft.Json package
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Infrastructure
{
    public class KundRepositoryJson : IKundRepository
    {
        private readonly string _filePath;

        public KundRepositoryJson(string filePath)
        {
            _filePath = filePath;
        }

        private List<Kund> LoadData()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Kund>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Kund>>(json) ?? new List<Kund>();
        }

        private void SaveData(List<Kund> kunder)
        {
            var json = JsonConvert.SerializeObject(kunder, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public Task<Kund?> GetByIdAsync(Guid id)
        {
            var kunder = LoadData();
            var kund = kunder.FirstOrDefault(k => k.Id == id);
            return Task.FromResult(kund);
        }

        public Task<IEnumerable<Kund>> GetAllAsync()
        {
            var kunder = LoadData();
            return Task.FromResult(kunder.AsEnumerable());
        }

        public Task<Kund?> ValidateKundAsync(string förnamn, string lösenord)
        {
            var kunder = LoadData();
            var kund = kunder.FirstOrDefault(k => k.Förnamn == förnamn && k.Lösenord == lösenord);
            return Task.FromResult(kund);
        }

        public Task AddAsync(Kund kund)
        {
            var kunder = LoadData();
            kunder.Add(kund);
            SaveData(kunder);
            return Task.CompletedTask;
        }
    }
}
