using Microsoft.AspNetCore.Mvc;
using BankApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BankApp.Controllers;

public class MinaSidorController : Controller
{
    // Mocked user data
    private const string MockedUsername = "user";
    private const string MockedPassword = "pass"; // Note: NEVER hard-code passwords in real applications.

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

}