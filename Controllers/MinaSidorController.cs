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

    // Inject the service through the ctor
    // Ärver in en instans av IKundService som är definierad i Application
    // Gör om till primary constructor
    public MinaSidorController(IKundService kundService)
    {
        _kundService = kundService;
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
        if (kund != null)
        {
            // Finns kund i databasen, claimas identitet och en kaka sätts för att hålla sessionen aktiv.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Förnamn),
                new Claim("KundId", kund.Id.ToString())
            };
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("start", "minasidor");
        }

        // Mocked user verification
        if (model.Förnamn == MockedUsername && model.Lösenord == MockedPassword)
        {

            // Set up the session/cookie for the authenticated user.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.Förnamn),
                new Claim("KundId", "12345678-1234-1234-1234-123456789ABC".ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Normally, here you'd set up the session/cookie for the authenticated user.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("start", "minasidor"); // Redirect to a secure area of your application.
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Generic error message for security reasons.
        return View(model);
    }

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

    public IActionResult Start()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> KundInfo()
    {
        // Hämta kundId/GUID från claim
        var kundIdClaim = User.Claims.FirstOrDefault(c => c.Type == "KundId")?.Value;
        if (string.IsNullOrEmpty(kundIdClaim) || !Guid.TryParse(kundIdClaim, out Guid kundId))
        {
            return RedirectToAction("login", "minasidor");
        }

        // Hämta kund från databasen med hjälp av GUID
        var kundDTO = await _kundService.GetKundByIdAsync(kundId);
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }

        var kundViewModel = new KundViewModel
        {
            Id = kundDTO.Id,
            Lösenord = kundDTO.Lösenord,         // Obs! Generellt sätt så är det ingen bra idé att visa lösenordet här :)
            Personnummer = kundDTO.Personnummer,
            Förnamn = kundDTO.Förnamn,
            Efternamn = kundDTO.Efternamn,
            Adress = kundDTO.Adress,
            Postnummer = kundDTO.Postnummer,
            Postort = kundDTO.Postort,
            Tele = kundDTO.Tele,
            Epost = kundDTO.Epost
        };

        return View(kundViewModel);
    }

    [HttpPost]
    public IActionResult KundInfo(KundViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Save to database or any other logic here
            // return RedirectToAction("Success"); // Redirect to a success page
            return View(model);
        }

        // If the model is not valid, return the same view to display errors
        return View(model);
    }
}




















