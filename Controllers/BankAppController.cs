using BankApp.Application;
using BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankApp.Controllers;

public class BankAppController : Controller
{
    private readonly ILogger<BankAppController> _logger;
    private readonly IKundService _kundService;

    // Kan göras om till primary constructor
    public BankAppController(ILogger<BankAppController> logger, IKundService kundService)
    {
        _logger = logger;
        _kundService = kundService;
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Nykund(KundDataModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var nyKund = new KundDTO
        {
            KundId = Guid.NewGuid(),
            Lösenord = model.Lösenord,
            Personnummer = model.Personnummer,
            Förnamn = model.Förnamn,
            Efternamn = model.Efternamn,
            Adress = model.Adress,
            Postnummer = model.Postnummer,
            Postort = model.Postort,
            Tele = model.Tele,
            Epost = model.Epost
        };

        await _kundService.AddKundAsync(nyKund);
        TempData["SuccessMessage"] = $"{model.Förnamn} {model.Efternamn} har registrerats.";
        return RedirectToAction("index", "bankapp");
    }

    public IActionResult Nykund()
    {
        return View(new KundDataModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Nyadmin(KundDataModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var nyKund = new KundDTO
        {
            KundId = Guid.NewGuid(),
            IsAdmin = true,
            Lösenord = model.Lösenord,
            Personnummer = model.Personnummer,
            Förnamn = model.Förnamn,
            Efternamn = model.Efternamn,
            Adress = model.Adress,
            Postnummer = model.Postnummer,
            Postort = model.Postort,
            Tele = model.Tele,
            Epost = model.Epost
        };

        await _kundService.AddKundAsync(nyKund);
        TempData["SuccessMessage"] = $"{model.Förnamn} {model.Efternamn} har registrerats.";
        return RedirectToAction("index", "bankapp");
    }

    public IActionResult Nyadmin()
    {
        return View(new KundDataModel());
    }
}