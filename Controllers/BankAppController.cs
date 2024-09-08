namespace BankApp.Controllers
{
    using BankApp.Application;
    using BankApp.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;


    public class BankAppController : Controller
    {
        private readonly ILogger<BankAppController> _logger;
        private readonly IKundService _kundService;

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

        public IActionResult Register()
    {
        return View();
    }

[HttpPost]

public async Task<IActionResult> Register(KundViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    // Create a new KundDTO object from the view model
    var kundDTO = new KundDTO
    {
        Id = model.Id,
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

    var kund = await _kundService.AddKundAsync(kundDTO);

    if (kund == null)
    {
        ModelState.AddModelError(string.Empty, "Kunden kunde inte skapas.");
        return View(model);
    }

    return RedirectToAction("Index");
}
}
}
