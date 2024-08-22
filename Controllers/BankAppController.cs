using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BankApp.Models;

namespace BankApp.Controllers;

public class BankAppController : Controller
{
    private readonly ILogger<BankAppController> _logger;

    public BankAppController(ILogger<BankAppController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
