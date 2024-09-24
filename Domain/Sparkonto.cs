namespace BankApp.Domain;

// Factory pattern för att skapa sparkonto
public class SparkontoFactory
{
    public Sparkonto CreateSparkonto(Guid kundId, decimal initialSaldo)
    {
        var sparkontoId = Guid.NewGuid();
        return new Sparkonto(sparkontoId, kundId, initialSaldo);
    }
}

// Sparkonto entity
public class Sparkonto
{
    public Guid SparkontoId { get; private set; }
    public Guid KundId { get; private set; }
    public decimal Saldo { get; private set; }

    // Sätt saldo för befintligt sparkonto
    public Sparkonto(Guid sparkontoId, Guid kundId, decimal initialSaldo)
    {
        SparkontoId = sparkontoId;
        KundId = kundId;
        Saldo = initialSaldo;
    }

    // Skapa ett nytt sparkonto med saldo 0
    public Sparkonto(Guid kundId)
    {
        SparkontoId = Guid.NewGuid();
        KundId = kundId;
        Saldo = 0;
    }

    // Skapa ett nytt sparkonto med valfritt saldo
    public Sparkonto(Guid kundId, decimal initialSaldo)
    {
        SparkontoId = Guid.NewGuid();
        KundId = kundId;
        Saldo = initialSaldo;
    }

    // Insättning
    public void Insattning(decimal belopp)
    {
        if (belopp <= 0) throw new ArgumentException("Beloppet måste vara positivt.");
        Saldo += belopp;
    }

    // Uttag
    public void Uttag(decimal belopp)
    {
        if (belopp <= 0) throw new ArgumentException("Beloppet måste vara positivt.");
        if (Saldo < belopp) throw new InvalidOperationException("Otillräckliga medel på kontot.");
        Saldo -= belopp;
    }

    // Hämta saldo
    public decimal GetSaldo()
    {
        return Saldo;
    }
}