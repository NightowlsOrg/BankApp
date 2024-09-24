using BankApp.Application;
using BankApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankApp.Controllers;

public class MinaSidorController : Controller
{
    private readonly IKundService _kundService;
    private readonly ISparkontoService _sparkontoService;

    // Ärver in en instans av IKundService som är definierad i Application
    // Ärver in en instans av ISparkontoService som är definierad i Application
    // Kan göras om till primary constructor
    public MinaSidorController(IKundService kundService, ISparkontoService sparkontoService)
    {
        _kundService = kundService;
        _sparkontoService = sparkontoService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        // Check model validators
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Validera om kund finns i databasen.
        var kund = await _kundService.ValidateKundAsync(model.Förnamn, model.Lösenord);
        if (kund != null)
        {
            // Finns kund i databasen, claimas identitet och en kaka sätts för att hålla sessionen aktiv.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Förnamn),
                new Claim("IsAdmin", kund.IsAdmin.ToString()),
                new Claim("KundId", kund?.KundId.ToString() ?? "12345678-1234-1234-1234-123456789ABC")
            };
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("start", "minasidor");
        }

        ModelState.AddModelError(string.Empty, "Felaktigt inloggningsförsök."); // Generiskt felmeddelande pga säkerhetskäl.
        return View(model);
    }

    [Authorize]
    public IActionResult AuthInfo()
    {
        return View();
    }

    [Authorize]
    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("index", "bankapp")
            },
            CookieAuthenticationDefaults.AuthenticationScheme);
    }

    // Hämta kund från databas med hjälp av kundid från claim
    [Authorize]
    public async Task<IActionResult> KundInfo()
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }

        // Hämta sparkonto för kund
        var sparkontoDTO = await _sparkontoService.GetSparkontoByKundIdAsync(kundDTO.KundId);

        var kundViewModel = new KundViewModel
        {
            KundId = kundDTO.KundId,
            IsAdmin = kundDTO.IsAdmin,
            Lösenord = kundDTO.Lösenord,    // SKA BORT
            Personnummer = kundDTO.Personnummer,
            Förnamn = kundDTO.Förnamn,
            Efternamn = kundDTO.Efternamn,
            Adress = kundDTO.Adress,
            Postnummer = kundDTO.Postnummer,
            Postort = kundDTO.Postort,
            Tele = kundDTO.Tele,
            Epost = kundDTO.Epost,
            Saldo = sparkontoDTO?.Saldo ?? -1
        };

        return PartialView(kundViewModel);
    }

    private async Task<KundDTO?> GetKundByClaimAsync()
    {
        var kundIdClaim = User.Claims.FirstOrDefault(c => c.Type == "KundId")?.Value;
        if (string.IsNullOrEmpty(kundIdClaim) || !Guid.TryParse(kundIdClaim, out Guid kundId))
        {
            return null;
        }

        return await _kundService.GetKundByIdAsync(kundId);
    }

    [HttpPost]
    public IActionResult KundInfo(KundViewModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("index", "bankapp");
        }

        return View(model);
    }

   public async Task<IActionResult> Update(Guid kundId)
   {
        var kund = await _kundService.GetKundByIdAsync(kundId);
        if (kund == null) return NotFound();

        var viewModel = new KundViewModel {
            KundId = kund.KundId, 
            IsAdmin = kund.IsAdmin, 
            Personnummer = kund.Personnummer, 
            Förnamn = kund.Förnamn, 
            Efternamn = kund.Efternamn, 
            Adress = kund.Adress, 
            Postnummer = kund.Postnummer, 
            Postort = kund.Postort, 
            Tele = kund.Tele, 
            Epost = kund.Epost, 
            Lösenord = kund.Lösenord
       };
        
        return PartialView("update", viewModel);
   }

   [HttpPost]
   public async Task<IActionResult> Update(KundViewModel model)
   {
       if (ModelState.IsValid)
       {
            var kund = new KundDTO
            { 
                KundId = model.KundId, 
                IsAdmin = model.IsAdmin, 
                Personnummer = model.Personnummer, 
                Förnamn = model.Förnamn, 
                Efternamn = model.Efternamn, 
                Adress = model.Adress, 
                Postnummer = model.Postnummer, 
                Postort = model.Postort, 
                Tele = model.Tele, 
                Epost = model.Epost, 
                Lösenord = model.Lösenord
            };

            await _kundService.UpdateKundAsync(kund);

            TempData["SuccessMessage"] = "Dina uppgifter har uppdaterats!";
            return RedirectToAction("meny", "minasidor");

        }

        return PartialView("update", model);
    }

    public async Task<IActionResult> Start()
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }
    
        return View("start", kundDTO.Förnamn);
    }

    // Skapa ett sparkonto för en kund
    public IActionResult CreateSparkonto(Guid kundId)
    {
        return View(new CreateSparkontoViewModel { KundId = kundId, Saldo = 0 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSparkonto(CreateSparkontoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var sparkontoDto = new SparkontoDTO
            {
                KundId = model.KundId, Saldo = model.Saldo
            };            
            await _sparkontoService.CreateSparkontoAsync(model.KundId, sparkontoDto);
            return RedirectToAction("meny", "minasidor");
        }

        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> Meny()
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }

        var sparkontoDTO = await _sparkontoService.GetSparkontoByKundIdAsync(kundDTO.KundId);

        var sparkontoViewModel = new SparkontoViewModel
        {
            SparkontoId = sparkontoDTO?.SparkontoId ?? Guid.Empty,
            Saldo = sparkontoDTO?.Saldo ?? 0,
            HasSparkonto = sparkontoDTO != null
        };

        return View("meny", sparkontoViewModel);
    }
}