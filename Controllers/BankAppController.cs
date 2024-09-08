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
    
}
