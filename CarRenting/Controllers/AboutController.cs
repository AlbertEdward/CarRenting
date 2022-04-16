using CarRenting.Services.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
