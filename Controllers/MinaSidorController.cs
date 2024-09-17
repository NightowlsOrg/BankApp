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
    // Mocked user data
    private const string MockedUsername = "user";
    private const string MockedPassword = "pass"; // Note: NEVER hard-code passwords in real applications.

    private readonly IKundService _kundService;
    private readonly ISparkontoService _sparkontoService;

    // Inject the service through the ctor
    // Ärver in en instans av IKundService som är definierad i Application
    // Gör om till primary constructor
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
    [ValidateAntiForgeryToken] // This ensures that the form is submitted with a valid anti-forgery token to prevent CSRF attacks.
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        // Check model validators
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Validera om kund finns i databasen.
        var kund = await _kundService.ValidateKundAsync(model.Förnamn, model.Lösenord);
        if (kund != null || (model.Förnamn == MockedUsername && model.Lösenord == MockedPassword))
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

        ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Generic error message for security reasons.
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

    // Hämta kund från databas med hjälp av kund-id från claim
    [Authorize]
    public async Task<IActionResult> KundInfo()
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }

        // Get Savings Account data for the customer
        var sparkonton = await _sparkontoService.GetByKundIdAsync(kundDTO.KundId);
        var firstSpartkonto = sparkonton.FirstOrDefault();

        var kundViewModel = new KundViewModel
        {
            KundId = kundDTO.KundId,
            IsAdmin = kundDTO.IsAdmin,
            Lösenord = kundDTO.Lösenord,         // Obs! Generellt sätt så är det ingen bra idé att visa lösenordet här :)
            Personnummer = kundDTO.Personnummer,
            Förnamn = kundDTO.Förnamn,
            Efternamn = kundDTO.Efternamn,
            Adress = kundDTO.Adress,
            Postnummer = kundDTO.Postnummer,
            Postort = kundDTO.Postort,
            Tele = kundDTO.Tele,
            Epost = kundDTO.Epost,
            Saldo = firstSpartkonto?.Saldo ?? 0 // Assuming the first account is being checked
        };

        return View(kundViewModel);
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
            // Save to database or any other logic here
            // return RedirectToAction("Success"); // Redirect to a success page
            return RedirectToAction("index", "bankapp");
        }

        // If the model is not valid, return the same view to display errors
        return View(model);
    }

   public async Task<IActionResult> Update(Guid kundId)
   {
       var kund = await _kundService.GetKundByIdAsync(kundId);
       if (kund == null) return NotFound();

       var viewModel = new KundViewModel { KundId = kund.KundId, IsAdmin = kund.IsAdmin, Personnummer = kund.Personnummer, Förnamn = kund.Förnamn, Efternamn = kund.Efternamn, Adress = kund.Adress, Postnummer = kund.Postnummer, Postort = kund.Postort, Tele = kund.Tele, Epost = kund.Epost, Lösenord = kund.Lösenord };
       return View(viewModel);
   }

   [HttpPost]
   public async Task<IActionResult> Update(KundViewModel model)
   {
       if (ModelState.IsValid)
       {
            var kund = new KundDTO { KundId = model.KundId, IsAdmin = model.IsAdmin, Personnummer = model.Personnummer, Förnamn = model.Förnamn, Efternamn = model.Efternamn, Adress = model.Adress, Postnummer = model.Postnummer, Postort = model.Postort, Tele = model.Tele, Epost = model.Epost, Lösenord = model.Lösenord };
            await _kundService.UpdateKundAsync(kund);
            return RedirectToAction("index", "bankapp");
       }
       
       return View(model);
   }

    // Sparkonto
    public async Task<IActionResult> Start()
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }

        // Redirect to opening an account if the customer doesn't have one yet
        var sparkonton = await _sparkontoService.GetByKundIdAsync(kundDTO.KundId);
        if (!sparkonton.Any() && !kundDTO.IsAdmin)
        {
            TempData["OpenSparkontoMessage"] = "Klicka här för att öppna ett sparkonto och få 1000 kr som välkomstbonus!";
            return View("start", kundDTO.Förnamn);
        }

        return View("start", kundDTO.Förnamn);
    }

    public async Task<IActionResult> OpenSparkonto()
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("index", "bankapp");
        }

        await _sparkontoService.OpenSparkontoAsync(kundDTO.KundId);
        TempData["SuccessMessage"] = "Ett sparkonto har öppnats och 1000 kr har satts in på kontot.";
        return RedirectToAction("index", "bankapp");
    }
}