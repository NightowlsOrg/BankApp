using BankApp.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IKundService _kundService;

        public AdminController(IKundService kundService)
        {
            _kundService = kundService;
        }

        public async Task<IActionResult> ListaKunder()
        {
            var kunder = await _kundService.GetAllAsync();
            return View(kunder);
        }
    }
}