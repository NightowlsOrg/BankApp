using BankApp.Domain;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Infrastructure;

public class SparkontoRepository : ISparkontoRepository
{
    private readonly AppDbContext _context;

    public SparkontoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Sparkonto?> GetByIdAsync(Guid id)
    {
        var sparkonto = await _context.Sparkonton.FindAsync(id);
        return sparkonto == null ? null : new Sparkonto(sparkonto.KundId, sparkonto.Saldo);
    }

    public async Task AddAsync(Sparkonto sparkonto)
    {
        var dataModel = new SparkontoDataModel
        {
            Id = sparkonto.Id,
            KundId = sparkonto.KundId,
            Saldo = sparkonto.Saldo
        };

        await _context.Sparkonton.AddAsync(dataModel);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Sparkonto>> GetByKundIdAsync(Guid kundId)
    {
        return await _context.Sparkonton
            .Where(a => a.KundId == kundId)
            .Select(a => new Sparkonto(a.KundId, a.Saldo))
            .ToListAsync();
    }
}

