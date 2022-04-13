using Microsoft.AspNetCore.Mvc;

namespace CarRenting.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
