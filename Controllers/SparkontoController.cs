using BankApp.Application;
using BankApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

public class SparkontoController : Controller
{
    private readonly ISparkontoService _sparkontoService;
    private readonly IKundService _kundService;

    // Kan göras om till primary constructor
    public SparkontoController(ISparkontoService sparkontoService, IKundService kundService)
    {
        _sparkontoService = sparkontoService;
        _kundService = kundService;
    }

    // Hämta kundid från claim
    private async Task<KundDTO?> GetKundByClaimAsync()
    {
        var kundIdClaim = User.Claims.FirstOrDefault(c => c.Type == "KundId")?.Value;
        if (string.IsNullOrEmpty(kundIdClaim) || !Guid.TryParse(kundIdClaim, out Guid kundId))
        {
            return null;
        }

        return await _kundService.GetKundByIdAsync(kundId);
    }

    // Visa saldo för sparkonto
    public async Task<IActionResult> Saldo(Guid sparkontoId)
    {
        var sparkonto = await _sparkontoService.GetSparkontoByIdAsync(sparkontoId);
        var viewModel = new SparkontoViewModel
        {
            SparkontoId = sparkontoId,
            Saldo = sparkonto?.Saldo ?? 0
        };

        return PartialView("Saldo", viewModel);
    }

    // Insättning på sparkonto
    public IActionResult Insattning(Guid sparkontoId)
    {
        return PartialView(new SparkontoViewModel { SparkontoId = sparkontoId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Insattning(Guid sparkontoId, decimal belopp)
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }

        var sparkontoDTO = await _sparkontoService.GetSparkontoByKundIdAsync(kundDTO.KundId);
        if (sparkontoDTO == null)
        {
            return NotFound("Inget sparkonto hittades.");
        }

        await _sparkontoService.InsattningAsync(sparkontoDTO.SparkontoId, belopp);

        TempData["SuccessMessage"] = $"{belopp:C} har satts in på kontot.";
        return RedirectToAction("meny", "minasidor");
    }


   // Uttag från sparkonto
    public IActionResult Uttag(Guid sparkontoId)
    {
        return PartialView(new SparkontoViewModel { SparkontoId = sparkontoId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Uttag(Guid sparkontoId, decimal belopp)
    {
        var kundDTO = await GetKundByClaimAsync();
        if (kundDTO == null)
        {
            return RedirectToAction("login", "minasidor");
        }

        var sparkontoDTO = await _sparkontoService.GetSparkontoByKundIdAsync(kundDTO.KundId);
        if (sparkontoDTO == null)
        {
            return NotFound("Inget sparkonto hittades.");
        }

        await _sparkontoService.UttagAsync(sparkontoDTO.SparkontoId, belopp);

        TempData["SuccessMessage"] = $"{belopp:C} har tagits ut från kontot.";
        return RedirectToAction("meny", "minasidor");
    }
}