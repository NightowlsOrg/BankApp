using BankApp.Domain;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Infrastructure;

public class SparkontoRepository : ISparkontoRepository
{
    private readonly AppDbContext _context;

    // Kan göras om till primary constructor
    public SparkontoRepository(AppDbContext context)
    {
        _context = context;
    }

    // Hämta en kunds sparkonto
    public async Task<Sparkonto?> GetByKundIdAsync(Guid kundId)
    {
        var dataModel = await _context.Sparkonton.FirstOrDefaultAsync(s => s.KundId == kundId);
        return dataModel != null ? new Sparkonto(dataModel.SparkontoId, dataModel.KundId, dataModel.Saldo) : null;
    }

    // Hämta ett sparkonto
    public async Task<Sparkonto?> GetByIdAsync(Guid sparkontoId)
    {
        var dataModel = await _context.Sparkonton.FindAsync(sparkontoId);
        return dataModel != null ? new Sparkonto(dataModel.SparkontoId, dataModel.KundId, dataModel.Saldo) : null;
    }
    
    // Lägg till ett sparkonto
    public async Task AddAsync(Sparkonto sparkonto)
    {
        var dataModel = new SparkontoDataModel
        {
            SparkontoId = sparkonto.SparkontoId,
            KundId = sparkonto.KundId,
            Saldo = sparkonto.Saldo
        };
        _context.Sparkonton.Add(dataModel);
        await _context.SaveChangesAsync();
    }

    // Hämta saldo för sparkonto
    public async Task<decimal> GetSaldoAsync(Guid sparkontoId)
    {
        var dataModel = await _context.Sparkonton.FindAsync(sparkontoId);
        return dataModel.Saldo;
    }

    // Insättning på sparkonto
    public async Task DepositAsync(Guid sparkontoId, decimal belopp)
    {
        var dataModel = await _context.Sparkonton.FindAsync(sparkontoId);
        dataModel.Saldo += belopp;
        await _context.SaveChangesAsync();
    }

    // Uttag från sparkonto
    public async Task UttagAsync(Guid sparkontoId, decimal belopp)
    {
        var dataModel = await _context.Sparkonton.FindAsync(sparkontoId);
        dataModel.Saldo -= belopp;
        await _context.SaveChangesAsync();
    }

    // Uppdatera sparkonto
    public async Task UpdateAsync(Sparkonto sparkonto)
    {
        var dataModel = await _context.Sparkonton.FindAsync(sparkonto.SparkontoId);
        dataModel.Saldo = sparkonto.Saldo;
        await _context.SaveChangesAsync();
    }
}