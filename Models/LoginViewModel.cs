using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class LoginViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string Förnamn { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Lösenord { get; set; } = string.Empty;
}