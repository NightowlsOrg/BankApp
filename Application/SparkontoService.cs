using BankApp.Domain;

namespace BankApp.Application;

public class SparkontoService : ISparkontoService
{
    private readonly ISparkontoRepository _sparkontoRepository;

    public SparkontoService(ISparkontoRepository sparkontoRepository)
    {
        _sparkontoRepository = sparkontoRepository;
    }

    public async Task<SparkontoDTO> OpenSparkontoAsync(Guid kundId)
    {
        var nyttSparkonto = new Sparkonto(kundId, 1000M); // Add $1000 welcome gift
        await _sparkontoRepository.AddAsync(nyttSparkonto);
        
        return new SparkontoDTO
        {
            SparkontoId = nyttSparkonto.SparkontoId,
            KundId = nyttSparkonto.KundId,
            Saldo = nyttSparkonto.Saldo
        };
    }

    public async Task<IEnumerable<SparkontoDTO>> GetByKundIdAsync(Guid kundId)
    {
        var sparkonton = await _sparkontoRepository.GetByKundIdAsync(kundId);
        return sparkonton.Select(sk => new SparkontoDTO
        {
            SparkontoId = sk.SparkontoId,
            KundId = sk.KundId,
            Saldo = sk.Saldo
        });
    }
}
