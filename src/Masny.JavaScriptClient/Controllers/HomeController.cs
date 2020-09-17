using Microsoft.AspNetCore.Mvc;

namespace Masny.JavaScriptClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
    }
}
