using BankApp.Application;
using BankApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly IKundService _kundService;

    // Kan göras om till primary constructor
    public AdminController(IKundService kundService)
    {
        _kundService = kundService;
    }

    public async Task<IActionResult> ListaKunder()
    {
        var kunder = await _kundService.GetAllKunderAsync();
        var viewModel = kunder.Select(k => new KundViewModel { 
            KundId = k.KundId, 
            IsAdmin =k.IsAdmin, 
            Personnummer = k.Personnummer, 
            Förnamn = k.Förnamn, 
            Efternamn = k.Efternamn, 
            Adress = k.Adress, 
            Postnummer = k.Postnummer, 
            Postort = k.Postort, 
            Tele = k.Tele, 
            Epost = k.Epost, 
            Lösenord = k.Lösenord });
            
        return View(kunder);
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
        Lösenord = kund.Lösenord };

       return View(viewModel);
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
            return RedirectToAction("listakunder", "admin");
       }
       
       return View(model);
   }

    public async Task<IActionResult> Delete(Guid kundId)
    {
        var kund = await _kundService.GetKundByIdAsync(kundId);
        if (kund == null) return NotFound();

        return View(new KundViewModel
        { 
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
        });
   }

   [HttpPost, ActionName("Delete")]
   public async Task<IActionResult> DeleteConfirmed(Guid kundId)
   {
        await _kundService.DeleteKundAsync(kundId);
        return RedirectToAction("listakunder", "admin");
   }
}