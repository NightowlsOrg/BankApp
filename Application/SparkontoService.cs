using BankApp.Domain;

namespace BankApp.Application;

public class SparkontoService : ISparkontoService
{
    private readonly IKundService _kundService;
    private readonly ISparkontoRepository _sparkontoRepository;

    // Kan göras om till primary constructor
    public SparkontoService(IKundService kundService, ISparkontoRepository sparkontoRepository)
    {
        _kundService = kundService;
        _sparkontoRepository = sparkontoRepository;
    }

    // Skapa sparkonto för kund
    public async Task CreateSparkontoAsync(Guid kundId, SparkontoDTO sparkontoDto)
    {
        var kund = await _kundService.GetKundByIdAsync(kundId);
        if (kund == null)
        {
            throw new InvalidOperationException("Den angivna kunden finns inte.");
        }

        var sparkonto = new Sparkonto(Guid.NewGuid(), kundId, sparkontoDto.Saldo);

        await _sparkontoRepository.AddAsync(sparkonto);
    }

    // Hämta sparkonto för kund
    public async Task<SparkontoDTO?> GetSparkontoByKundIdAsync(Guid kundId)
    {
        var sparkonto = await _sparkontoRepository.GetByKundIdAsync(kundId);
        return sparkonto != null ? new SparkontoDTO { SparkontoId = sparkonto.SparkontoId, KundId = sparkonto.KundId, Saldo = sparkonto.Saldo } : null;
    }

    // Hämta sparkonto med id
    public async Task<SparkontoDTO?> GetSparkontoByIdAsync(Guid sparkontoId)
    {
        var sparkonto = await _sparkontoRepository.GetByIdAsync(sparkontoId);

        if (sparkonto != null)
        {
            return new SparkontoDTO
            {
                SparkontoId = sparkonto.SparkontoId,
                KundId = sparkonto.KundId,
                Saldo = sparkonto.Saldo
            };
        }

        return null;
    }

    // Hämta saldo för sparkonto
    public async Task<decimal> GetSaldoAsync(Guid sparkontoId)
    {
        var sparkonto = await _sparkontoRepository.GetByIdAsync(sparkontoId);

        if (sparkonto == null)
        {
            throw new InvalidOperationException("Inget sparkonto kunde hittas.");
        }

        return sparkonto.Saldo;
    }

    // Insättning på sparkonto
    public async Task InsattningAsync(Guid sparkontoId, decimal belopp)
    {
        if (belopp <= 0)
        {
            throw new ArgumentException("Insatt belopp måste vara större än 0.");
        }

        var sparkonto = await _sparkontoRepository.GetByIdAsync(sparkontoId);

        if (sparkonto == null)
        {
            throw new InvalidOperationException("Inget sparkonto kunde hittas.");
        }

        sparkonto.Insattning(belopp);

        await _sparkontoRepository.UpdateAsync(sparkonto);
    }

    // Uttag från sparkonto
    public async Task UttagAsync(Guid sparkontoId, decimal belopp)
    {
        if (belopp <= 0)
        {
            throw new ArgumentException("Uttaget belopp måste vara större än 0.");
        }

        var sparkonto = await _sparkontoRepository.GetByIdAsync(sparkontoId);

        if (sparkonto == null)
        {
            throw new InvalidOperationException("Inget sparkonto kunde hittas.");
        }

        sparkonto.Uttag(belopp);

        await _sparkontoRepository.UpdateAsync(sparkonto);
    }
}