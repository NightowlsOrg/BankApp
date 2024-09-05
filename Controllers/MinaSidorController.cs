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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        // Validera inloggningsuppgifter. Om validering misslyckas, returnera vyn med felmeddelande.
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Validera om kund finns i databasen.
        var kund = await _kundService.ValidateKundAsync(model.Förnamn, model.Lösenord);
        if (kund != null)
        {
            // Om kund finns i databasen, claima identity, sätt upp session/cookie och skicka vidare till mina sidor.
            var claims = new[] { new Claim(ClaimTypes.Name, model.Förnamn) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("start", "minasidor");
        }

        // Om validering misslyckas, använd mockade användaruppgifter.
        if (model.Förnamn == MockedUsername && model.Lösenord == MockedPassword)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, model.Förnamn) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("start", "minasidor");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
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

}