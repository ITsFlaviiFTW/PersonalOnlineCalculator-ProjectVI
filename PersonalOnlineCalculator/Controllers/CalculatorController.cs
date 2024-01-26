using Microsoft.AspNetCore.Mvc;

namespace PersonalOnlineCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
