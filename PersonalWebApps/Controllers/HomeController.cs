using Microsoft.AspNetCore.Mvc;

namespace tracker_webapp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
