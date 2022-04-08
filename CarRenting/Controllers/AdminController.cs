using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
