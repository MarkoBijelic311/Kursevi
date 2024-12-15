using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kursevi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Team()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

    }
}
