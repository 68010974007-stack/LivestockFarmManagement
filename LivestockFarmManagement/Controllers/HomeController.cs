using Microsoft.AspNetCore.Mvc;

namespace LivestockFarmManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}