using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
