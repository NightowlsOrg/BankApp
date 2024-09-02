using System.ComponentModel.DataAnnotations;

namespace BankApp.Models;

public class LoginViewModel
{
    [Required]
    public string? Användarnamn { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Lösenord { get; set; }
}