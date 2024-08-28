namespace BankApp.Models;
  public class Konto
  {
      public int KontoNummer { get; set; }
      public string? KontoTyp { get; set; }
      public decimal Saldo { get; set;}
      public string? Transaktion { get; set; }
  }