using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
