using Microsoft.AspNetCore.Mvc;

namespace Kursevi.Controllers
{
    public class KurseviController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
