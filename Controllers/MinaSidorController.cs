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

        // Mocked user verification
        if (model.Användarnamn == MockedUsername && model.Lösenord == MockedPassword)
        {

            // Set up the session/cookie for the authenticated user.
            var claims = new[] { new Claim(ClaimTypes.Name, model.Användarnamn) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Normally, here you'd set up the session/cookie for the authenticated user.
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
                RedirectUri = Url.Action("Index", "BankApp")
            },
            CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public IActionResult Start()
    {
        return View();
    }

   // Change the KundInfo method to use the service and the DTO to retreive kund info
    public IActionResult KundInfo()
    {
        // Controller är första stället att mocka kundobjektet medans man jobbar sig ner mot databasen
        // var kund = new Kund
        // {
        //     Personnummer = "1968-08-06",
        //     Förnamn = "Controller",
        //     Efternamn = "MinaSidor",
        //     Adress = "Gatan 1",
        //     Postnummer = "123 45",
        //     Postort = "Staden",
        //     Tele = "070-123 45 67",
        //     Epost = "epost@domain.se"
        // };

        // Hämta kund från Application service 
        var kundDTO = _kundService.GetKund();
        var kund = new Kund
        {
            Personnummer = kundDTO.Personnummer,
            Förnamn = kundDTO.Förnamn,
            Efternamn = kundDTO.Efternamn,
            Adress = kundDTO.Adress,
            Postnummer = kundDTO.Postnummer,
            Postort = kundDTO.Postort,
            Tele = kundDTO.Tele,
            Epost = kundDTO.Epost
        };

        return View(kund);
    }

    [HttpPost]
    public IActionResult KundInfo(Kund model)
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